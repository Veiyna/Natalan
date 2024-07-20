﻿using System;
using System.Collections.Generic;
using Shared.Network;
using WorldServer.Data;
using WorldServer.Game.Entity;

namespace WorldServer.Network
{
    [Session(ConnectionChannel.World)]
    public class WorldSession : Session
    {
        public WorldSession()
        {
            oodle = new Oodle();
        }
        public Player Player { get; set; }
        
        protected override bool CanProcessSubPacket(SubPacket subPacket)
        {
            SubPacketHandlerAttribute attribute = PacketManager.GetSubPacketHandlerInfo(subPacket);
            if (attribute == null)
                return true;

            if ((attribute.Flags & SubPacketHandlerFlags.RequiresPlayer) != 0 && Player == null)
            {
                #if DEBUG
                    Console.WriteLine($"Rejecting packet ({subPacket.SubHeader.Type}, {subPacket.Handler}), world session ({Remote}) doesn't have an assigned character!");
                #endif
                return false;
            }

            if ((attribute.Flags & SubPacketHandlerFlags.RequiresWorld) != 0 && (Player == null || !Player.InWorld))
            {
                #if DEBUG
                    Console.WriteLine($"Rejecting packet ({subPacket.SubHeader.Type}, {subPacket.Handler}), world session ({Remote}) character isn't in the world!");
                #endif
                return false;
            }

            if ((attribute.Flags & SubPacketHandlerFlags.RequiresParty) != 0 && Player.Party == null)
            {
                #if DEBUG
                    Console.WriteLine($"Rejecting packet ({subPacket.SubHeader.Type}, {subPacket.Handler}), world session ({Remote}) character isn't in a party!");
                #endif
                return false;
            }

            return true;
        }

        public override void Send(SubPacket subPacket)
        {
            uint actorId = Player?.Character.ActorId ?? 0u;
            Send(actorId, actorId, subPacket);
        }

        public void Send(IEnumerable<SubPacket> subPackets)
        {
            foreach (SubPacket subPacket in subPackets)
                Send(subPacket);
        }

        public override void Disconnect()
        {
            base.Disconnect();
            Player?.OnLogout();
        }
    }
}
