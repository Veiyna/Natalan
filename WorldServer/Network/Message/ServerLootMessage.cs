using System.IO;
using Shared.Game.Enum;
using Shared.Network;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerLootMessage)]
    public class ServerLootMessage : SubPacket
    {

        public LootMessageType MessageType;
        public uint Var1;
        public uint Var2;
        public uint Var3;
        public uint Var4;
        public uint Var5;
        public uint Var6;
        public uint Var7;
        public override void Write(BinaryWriter writer)
        {
            writer.Write((byte)this.MessageType);
            writer.Pad(3u);
            writer.Write(this.Var1);
            writer.Write(this.Var2);
            writer.Write(this.Var3);
            writer.Write(this.Var4);
            writer.Write(this.Var5);
            writer.Write(this.Var6);
            writer.Write(this.Var7);
        }
    }
}