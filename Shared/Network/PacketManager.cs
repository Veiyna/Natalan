using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Shared.Network
{
    public static class PacketManager
    {
        private static readonly Dictionary<SubPacketType, (Type Type, SubPacketAttribute Attribute)> typeSubPackets = new();
        private static readonly Dictionary<SubPacketClientHandlerId, (Type Type, SubPacketAttribute Attribute)> opcodeClientSubPackets = new();
        private static readonly Dictionary<SubPacketServerHandlerId, (Type Type, SubPacketAttribute Attribute)> opcodeServerSubPackets = new();

        public delegate void SubPacketHandler(Session session, SubPacket subPacket);
        private static readonly Dictionary<SubPacketType, (SubPacketHandler Handler, SubPacketHandlerAttribute Attribute)> subPacketTypeHandlers = new();
        private static readonly Dictionary<SubPacketClientHandlerId, (SubPacketHandler Handler, SubPacketHandlerAttribute Attribute)> subPacketOpcodeHandlers = new();

        public static Dictionary<SubPacketServerHandlerId, ushort> SharedOpcodesServer = new();
        public static Dictionary<ushort, SubPacketClientHandlerId> SharedOpcodesClient = new();
        
        public static Dictionary<SubPacketServerHandlerId, ushort> SharedOpcodesServerChat = new();
        public static Dictionary<ushort, SubPacketClientHandlerId> SharedOpcodesClientChat = new();
        public static void Initialise()
        {
            InitialiseStaticOpcodes();
            InitialisePackets();
            InitialisePacketHandlers();
        }

        private static void InitialiseStaticOpcodes()
        {
            //TODO: These should probably be an enum
            SharedOpcodesServer.Add(SubPacketServerHandlerId.ServerError, 0x2);
            SharedOpcodesServer.Add(SubPacketServerHandlerId.ServerServiceAccountList, 0xC);
            SharedOpcodesServer.Add(SubPacketServerHandlerId.ServerCharacterList, 0xD);
            SharedOpcodesServer.Add(SubPacketServerHandlerId.ServerCharacterCreate, 0xE);
            SharedOpcodesServer.Add(SubPacketServerHandlerId.ServerEnterWorld, 0xF);
            SharedOpcodesServer.Add(SubPacketServerHandlerId.ServerRealmList, 0x15);
            SharedOpcodesServer.Add(SubPacketServerHandlerId.ServerRetainerList, 0x17);
            SharedOpcodesClient.Add(0x3, SubPacketClientHandlerId.ClientCharacterList);
            SharedOpcodesClient.Add(0x4, SubPacketClientHandlerId.ClientEnterWorld);
            SharedOpcodesClient.Add(0x5, SubPacketClientHandlerId.ClientLobbyRequest);
            SharedOpcodesClient.Add(0xA, SubPacketClientHandlerId.ClientCharacterDelete);
            SharedOpcodesClient.Add(0xB, SubPacketClientHandlerId.ClientCharacterCreate);
            
            
            //Chat
            SharedOpcodesClientChat.Add(0x0065, SubPacketClientHandlerId.ClientChannelChat);
            SharedOpcodesServerChat.Add(SubPacketServerHandlerId.ServerChannelChat, 0x0065);
            
            SharedOpcodesClientChat.Add(0x0064, SubPacketClientHandlerId.ClientTell);
            SharedOpcodesServerChat.Add(SubPacketServerHandlerId.ServerTell, 0x0064);
            SharedOpcodesServerChat.Add(SubPacketServerHandlerId.ServerTellNotFound, 0x0066);
            
        }

        private static void InitialisePackets()
        {
            var sw = new Stopwatch();
            sw.Start();

            foreach (Type type in Assembly.GetEntryAssembly().GetTypes().Concat(Assembly.GetExecutingAssembly().GetTypes()))
            {
                foreach (SubPacketAttribute attribute in type.GetCustomAttributes<SubPacketAttribute>())
                {
                    if (attribute.ClientHandlerId != SubPacketClientHandlerId.None)
                        opcodeClientSubPackets[attribute.ClientHandlerId] = (type, attribute);
                    else if (attribute.ServerHandlerId != SubPacketServerHandlerId.None)
                        opcodeServerSubPackets[attribute.ServerHandlerId] = (type, attribute);
                    else if (attribute.Type != SubPacketType.None)
                        typeSubPackets[attribute.Type] = (type, attribute);
                }
            }

            Console.WriteLine($"Initialised {typeSubPackets.Count + opcodeClientSubPackets.Count + opcodeServerSubPackets.Count} packet(s) in {sw.ElapsedMilliseconds}ms.");
        }

        private static void InitialisePacketHandlers()
        {
            var sw = new Stopwatch();
            sw.Start();

            foreach (Type type in Assembly.GetEntryAssembly().GetTypes().Concat(Assembly.GetExecutingAssembly().GetTypes()))
            {
                foreach (MethodInfo method in type.GetMethods())
                {
                    foreach (SubPacketHandlerAttribute attribute in method.GetCustomAttributes<SubPacketHandlerAttribute>())
                    {
                        ParameterInfo[] handlerParameters = method.GetParameters();
                        Debug.Assert(handlerParameters.Length == 2);
                        Debug.Assert(handlerParameters[0].ParameterType == typeof(Session) || handlerParameters[0].ParameterType.IsSubclassOf(typeof(Session)));
                        Debug.Assert(handlerParameters[1].ParameterType == typeof(SubPacket) || handlerParameters[1].ParameterType.IsSubclassOf(typeof(SubPacket)));

                        ParameterExpression sessionParameter   = Expression.Parameter(typeof(Session));
                        ParameterExpression subPacketParameter = Expression.Parameter(typeof(SubPacket));
                        MethodCallExpression callExpression = Expression.Call(method,
                            Expression.Convert(sessionParameter, handlerParameters[0].ParameterType),
                            Expression.Convert(subPacketParameter, handlerParameters[1].ParameterType));

                        Expression<SubPacketHandler> lambda = Expression.Lambda<SubPacketHandler>(callExpression, sessionParameter, subPacketParameter);

                        SubPacketHandler handler = lambda.Compile();
                        if (attribute.ClientHandlerId != SubPacketClientHandlerId.None)
                            subPacketOpcodeHandlers[attribute.ClientHandlerId] = (handler, attribute);
                        else if (attribute.Type != SubPacketType.None)
                            subPacketTypeHandlers[attribute.Type] = (handler, attribute);
                    }
                }
            }

            Console.WriteLine($"Initialised {subPacketTypeHandlers.Count + subPacketOpcodeHandlers.Count} packet handler(s) in {sw.ElapsedMilliseconds}ms.");
        }

        private static (Type Type, SubPacketAttribute Attribute) GetSubPacketInfo(SubPacketType type, SubPacketClientHandlerId clientHandlerId, SubPacketServerHandlerId serverHandlerId)
        {
            (Type Type, SubPacketAttribute Attribute) subPacketInfo;
            if (clientHandlerId != SubPacketClientHandlerId.None)
                opcodeClientSubPackets.TryGetValue(clientHandlerId, out subPacketInfo);
            else if (serverHandlerId != SubPacketServerHandlerId.None)
                opcodeServerSubPackets.TryGetValue(serverHandlerId, out subPacketInfo);
            else
                typeSubPackets.TryGetValue(type, out subPacketInfo);

            return subPacketInfo;
        }

        public static SubPacketHandlerAttribute GetSubPacketHandlerInfo(SubPacket subPacket)
        {
            return _GetSubPacketHandlerInfo(subPacket).Attribute;
        }

        private static (SubPacketHandler Handler, SubPacketHandlerAttribute Attribute) _GetSubPacketHandlerInfo(SubPacket subPacket)
        {
            Debug.Assert(subPacket != null);

            (SubPacketHandler Handler, SubPacketHandlerAttribute Attribute) info;
            if (subPacket.Handler != SubPacketClientHandlerId.None)
                subPacketOpcodeHandlers.TryGetValue(subPacket.Handler, out info);
            else
                subPacketTypeHandlers.TryGetValue(subPacket.SubHeader.Type, out info);

            return info;
        }

        public static SubPacket GetSubPacket(SubPacketType type, SubPacketClientHandlerId clientHandlerId, SubPacketServerHandlerId serverHandlerId)
        {
            (Type Type, SubPacketAttribute Attribute) subPacketInfo = GetSubPacketInfo(type, clientHandlerId, serverHandlerId);
            return subPacketInfo.Type != null ? (SubPacket)Activator.CreateInstance(subPacketInfo.Type) : null;
        }

        public static void InvokeHandler(Session session, SubPacket subPacket)
        {
            CLogPacket(SubPacketDirection.Client, subPacket);

            (SubPacketHandler Handler, SubPacketHandlerAttribute Attribute) info = _GetSubPacketHandlerInfo(subPacket);
            info.Handler?.Invoke(session, subPacket);
        }

        [Conditional("DEBUG")]
        public static void CLogPacket(SubPacketDirection direction, SubPacket subPacket)
        {
            (Type Type, SubPacketAttribute Attribute) subPacketInfo;
            if (direction == SubPacketDirection.Client)
            {
                subPacketInfo = GetSubPacketInfo(subPacket.SubHeader.Type, subPacket.Handler, SubPacketServerHandlerId.None);
                if(subPacketInfo.Type == null || subPacketInfo.Attribute.Log)
                    Console.WriteLine($"Packet(C->S) - Size: {subPacket.SubHeader.Size}, Type: {subPacket.SubHeader.Type}, Handler: {subPacket.Handler}, Opcode: 0x{subPacket.SubMessageHeader.Opcode:x}");
            }
            else
            {
                subPacketInfo = GetSubPacketInfo(subPacket.SubHeader.Type, SubPacketClientHandlerId.None, subPacket.HandlerId);
                if (subPacketInfo.Type == null || subPacketInfo.Attribute.Log)
                    Console.WriteLine($"Packet(S->C) - Size: {subPacket.SubHeader.Size}, Type: {subPacket.SubHeader.Type}, Handler: {subPacket.HandlerId}, Opcode: 0x{subPacket.SubMessageHeader.Opcode:x}");
            }
        }
    }
}
