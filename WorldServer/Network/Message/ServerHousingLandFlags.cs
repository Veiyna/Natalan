using System.IO;
using Shared.Network;
using WorldServer.Game.Housing;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerHousingLandFlags)]
    public class ServerHousingLandFlags : SubPacket
    {
        public LandFlagSet[] LandFlagSets;
        public override void Write(BinaryWriter writer)
        {
            writer.Write(this.LandFlagSets[0].Marshal());
            writer.Write((ulong)0);
            writer.Write(this.LandFlagSets[1].Marshal());
            writer.Write((ulong)0);
            writer.Write(this.LandFlagSets[2].Marshal());
            writer.Write((ulong)0);
            writer.Write(this.LandFlagSets[3].Marshal());
            writer.Write(this.LandFlagSets[4].Marshal());
            writer.Write((ulong)0);
            writer.Write(this.LandFlagSets[5].Marshal());
            writer.Write((ulong)0);

        }

    }
}