using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Shared.Cryptography;
using System.Net;
using System.Net.Sockets;
using Shared.Game;
using Shared.Network.Message;
using WorldServer.Data;

namespace Shared.Network
{
    public abstract class Session
    {
        private const uint MaxIncomingPacketsPerUpdate   = 5;
        private const uint MaxOutgoingSubPacketsPerFlush = 20;
        
        public ConnectionChannel Channel { get; private set; }
        public IPAddress Local => connection.Local;
        public IPAddress Remote => connection.Remote;
        public ConnectionHeartbeat Heartbeat { get; } = new();

        private readonly Queue<IBaseEvent> events = new();
        
        protected Blowfish blowfish;
        protected Oodle oodle;
        protected Connection connection;
        public VersionData Version;


        private bool pendingDisconnect;
        private readonly ConcurrentQueue<Packet> incomingPackets = new();
        private readonly ConcurrentQueue<PendingSubPacket> outgoingPackets = new();
        
        public void Accept(Connection newConnection, ConnectionChannel channel)
        {
            connection = newConnection;
            Channel    = channel;
            
            // call this directly to finish processing the first packet
            ReceiveCallback();
            
        }

        public virtual SubPacketClientHandlerId ClientOpcodeToHandler(ushort opcode)
        {
            var sharedopcode = PacketManager.SharedOpcodesClient.GetValueOrNull(opcode);
            if (sharedopcode != null)
                return sharedopcode.Value;
            if (this.Version != null)
                return this.Version.ClientOpcodes.FirstOrDefault(i => i.Value == opcode).Key;

            return SubPacketClientHandlerId.None;

        }
        
        public virtual ushort ServerHandlerToOpcode(SubPacketServerHandlerId handlerId)
        {
            var sharedopcode = PacketManager.SharedOpcodesServer.GetValueOrNull(handlerId);
            if (sharedopcode != null)
                return sharedopcode.Value;
            if (this.Version != null && this.Version.ServerOpcodes.ContainsKey(handlerId))
                return this.Version.ServerOpcodes[handlerId];

            return 0;

        }

        private void ReceiveCallback()
        {
            if (pendingDisconnect)
                return;
            
            if (connection.ReceiveLength <= 0)
            {
                Console.WriteLine("Error while receiving packets. Disconnecting.");
                Disconnect();
                return;
            }

            byte[] payload = connection.Buffer.Copy(0, connection.ReceiveLength);
            
            // TODO: fragmented packets
            var packet = new Packet();
            if (packet.Process(payload, this, blowfish, oodle) != PacketResult.Ok)
            {
                Console.WriteLine("Error while receiving packets. Disconnecting.");
                Disconnect();
                return;
            }

            incomingPackets.Enqueue(packet);

            try
            {
                connection.BeginReceive(ReceiveCallback);
            }
            catch (SocketException exception)
            {
                #if DEBUG
                    Console.WriteLine(exception.Message);
                #endif
                Disconnect();
            }
        }

        public abstract void Send(SubPacket subPacket);

        public void Send(uint source, uint target, SubPacket subPacket)
        {
            outgoingPackets.Enqueue(new PendingSubPacket(subPacket, source, target));
        }

        public virtual void Disconnect()
        {
            pendingDisconnect = true;
            
            #if DEBUG
                Console.WriteLine($"Disconnected: {Remote}");
            #endif
            
            connection.Close();
            outgoingPackets.Clear();
            incomingPackets.Clear();
            
            NetworkManager.RemoveSession(this);
        }
        
        public void FlushPacketQueue()
        {
            if (outgoingPackets.Count == 0 || pendingDisconnect)
                return;

            try
            {
                IEnumerable<PendingSubPacket> outgoingSubPackets = outgoingPackets.DequeueMultiple(MaxOutgoingSubPacketsPerFlush);
                connection.Send(Packet.Build(this, blowfish, oodle, outgoingSubPackets));
            }
            catch (SocketException exception)
            {
                #if DEBUG
                    Console.WriteLine(exception.Message);
                #endif
                Disconnect();
            }
        }

        protected virtual bool CanProcessSubPacket(SubPacket subPacket)
        {
            return true;
        }

        /// <summary>
        /// Queue an event to be executed in a delayed threadsafe manner.
        /// </summary>
        public void NewEvent(IBaseEvent basicEvent)
        {
            events.Enqueue(basicEvent);
        }

        public virtual void Update(double lastTick)
        {
            foreach (Packet packet in incomingPackets.DequeueMultiple(MaxIncomingPacketsPerUpdate))
            {
                foreach (SubPacket subPacket in packet.SubPackets)
                {
                    if (!CanProcessSubPacket(subPacket))
                        continue;

                    try
                    {
                        PacketManager.InvokeHandler(this, subPacket);
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                    }
                }
            }

            while (events.Count > 0)
            {
                IBaseEvent nextEvent = events.Peek();
                if (!nextEvent.CanExecute())
                    break;

                events.Dequeue();
                nextEvent.Execute();
            }

            (ConnectionHeartbeatResult result, uint pulseTime) = Heartbeat.Update(lastTick);
            switch (result)
            {
                case ConnectionHeartbeatResult.Pulse:
                {
                    Send(0u, 0u, new KeepAliveRequest
                    {
                        Check     = (uint)Guid.NewGuid().GetHashCode(),
                        Timestamp = pulseTime
                    });
                    break;
                }
                case ConnectionHeartbeatResult.Flatline:
                    /*
                    Console.WriteLine("Connection flatline. Disconnecting.");
                    Disconnect();*/
                    break;
            }
            
            FlushPacketQueue();
        }
    }
}
