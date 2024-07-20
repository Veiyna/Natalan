using System.IO;
using Shared.Network;
using WorldServer.Game.Entity;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerCFRegister)]
    public class ServerCFRegister : SubPacket
    {
        public Player Player;
        public ushort[] Duties;
        
        public override void Write(BinaryWriter writer)
        {
            writer.Write((byte)1);
            writer.Write(this.Player.Character.ClassJobId);
            writer.Write(this.Player.Character.SocialInfo.SelectRegion);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write((byte)66); // 98 - reserving server
            writer.Write((byte)4);
            writer.Write((byte)6);
            writer.Write((byte)64);
            writer.Write((byte)1);
            writer.Write((byte)1);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write((byte)5);
            writer.Write((byte)1);
            writer.Write((uint)Duties[0]);
            writer.Write((uint)Duties[1]);
            writer.Write((uint)Duties[2]);
            writer.Write((uint)Duties[3]);
            writer.Write((uint)Duties[4]);
        }
    }
}