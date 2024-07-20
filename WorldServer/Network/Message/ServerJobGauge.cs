using System.IO;
using Shared.Network;
using WorldServer.Game.Entity;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerJobGauge)]
    public class ServerJobGauge : SubPacket
    {
        public Player Player;
        public override void Write(BinaryWriter writer)
        {
            writer.Write(this.Player.Character.ClassJobId);
            writer.Write(this.Player.JobGauge);
        }
    }
}