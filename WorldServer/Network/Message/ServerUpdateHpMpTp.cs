using System.IO;
using Shared.Network;
using WorldServer.Game.Entity;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerUpdateHpMpTp)]
    public class ServerUpdateHpMpTp : SubPacket
    {
        public Character Character;

        public override void Write(BinaryWriter writer)
        {
            writer.Write(this.Character.HP);
            writer.Write((ushort)this.Character.MP);
            writer.Write((ushort)10000);
            writer.Pad(8u);
        }
    }
}