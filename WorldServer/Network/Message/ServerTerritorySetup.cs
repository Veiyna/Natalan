using System.IO;
using Shared.Game;
using Shared.Network;
using WorldServer.Game.Map;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerTerritorySetup)]
    public class ServerTerritorySetup : SubPacket
    {
        public byte WeatherId;
        public WorldPosition WorldPosition;
        public Territory Territory;

        public override void Write(BinaryWriter writer)
        {
            writer.Write((ushort)0);
            writer.Write(WorldPosition.TerritoryId);
            writer.Write((ushort)this.WorldPosition.InstanceId);
            if(this.Territory.Director != null)
                writer.Write(this.Territory.Director.ContentFinderConditionId);
            else
            {
                writer.Write((ushort)0);
            }

            if (this.WorldPosition.TerritoryId == 212)
            {
                //writer.Write(90338u); The Scions of the Seventh Dawn
                //writer.Write(90339u); // Dressed to Deceive, Life Materia and everything (unchanged), Lord of the inferno
                //writer.Write(90340u); // A hero in the making
                //writer.Write(90341u); // till sea swallows all
                //writer.Write(90342u); // back from the wood 1
                writer.Write(90343u); // back from the wood 2, foot in the door, back to square one
                writer.Write(4107477u);
            }
            else
            {
                writer.Write(0u);
                writer.Write(0u);
            }

            writer.Write(WeatherId);
            writer.Write((byte)1);
            writer.Write((byte)16);
            writer.Write((byte)0);
            writer.Write(0u);
            writer.Write((ushort)0);
            writer.Write((ushort)0);
            writer.Write(0u);
            writer.Write(0u);
            writer.Write(0u);
            writer.Write(148u);
            writer.Write(0u);
            writer.Write(0u);
            writer.Write(0u);
            writer.Pad(12u);
            writer.Write(WorldPosition.Offset.X);
            writer.Write(WorldPosition.Offset.Y);
            writer.Write(WorldPosition.Offset.Z);
            writer.Pad(12u);
            writer.Write(0u);
        }
    }
}
