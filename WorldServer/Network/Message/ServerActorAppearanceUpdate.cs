using System.Collections.Generic;
using System.IO;
using Shared.Database.Datacentre;
using Shared.Network;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerActorAppearanceUpdate)]
    public class ServerActorAppearanceUpdate : SubPacket
    {
        public CharacterInfo Character;
        public ulong MainHandDisplayId;
        public ulong OffHandDisplayId;
        public IEnumerable<uint> VisibleItemDisplayIds;
        public IEnumerable<ushort> VisibleSecondDyeIds;

        public override void Write(BinaryWriter writer)
        {
            writer.Write(MainHandDisplayId);
            writer.Write(OffHandDisplayId);
            writer.Write((byte)0);
            writer.Write(Character.ClassId);
            writer.Write((byte)Character.GetClassInfo(Character.ClassJobId).Level);
            writer.Pad(1u);

            foreach (uint displayId in VisibleItemDisplayIds)
                writer.Write(displayId);
            
            foreach (uint displayId in this.VisibleSecondDyeIds)
                writer.Write((byte)displayId);
        }
    }
}
