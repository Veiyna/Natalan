using System.IO;
using Shared.Network;
using WorldServer.Game.Housing;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketServerHandlerId.ServerHousingUpdateLandFlagsSlot)]
public class ServerHousingUpdateLandFlagsSlot : SubPacket
{
    public Land Land;
    public override void Write(BinaryWriter writer)
    {
            writer.Write((uint)2);
            writer.Write((uint)0);
            writer.Write(this.Land.LandIdent.Marshal());
            writer.Write((uint)0);
            writer.Write((uint)0);
        }
        
}