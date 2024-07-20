using Shared.Network;
using System.IO;
using WorldServer.Game.ContentFinder;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerContentFinderNotify)]
    public class ServerContentFinderNotify : SubPacket
    {
        public uint State;
        public uint CancelledReason = 0;
        public byte ClassJobId;
        public ushort ContentId;
        public ContentGroup ContentGroup;
        public ContentFinderLanguage Languages;

        public override void Write(BinaryWriter writer)
        {
            writer.Write(State);
            writer.Write(CancelledReason);
            writer.Write((byte)98);
            writer.Write((byte)36);
            writer.Write((byte)130);
            writer.Write((byte)64);
            writer.Pad(8u);
            writer.Write((ushort)0);
            writer.Write((ushort)0);
            writer.Write((ushort)1);
            writer.Write((ushort)0);
            writer.Write(this.ContentId);
            writer.Write((ushort)0);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write((byte)this.ContentGroup.Players.Count);
            
        }
    }
}
