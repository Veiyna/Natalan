using System;

namespace Shared.Network
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class SubPacketHandlerAttribute : Attribute
    {
        public SubPacketClientHandlerId ClientHandlerId { get; }
        public SubPacketType Type { get; }
        public SubPacketHandlerFlags Flags { get; }

        public SubPacketHandlerAttribute(SubPacketClientHandlerId handlerId, SubPacketHandlerFlags flags = SubPacketHandlerFlags.None)
        {
            this.ClientHandlerId = handlerId;
            Flags        = flags;
        }

        public SubPacketHandlerAttribute(SubPacketType type, SubPacketHandlerFlags flags = SubPacketHandlerFlags.None)
        {
            Type  = type;
            Flags = flags;
        }
    }
}
