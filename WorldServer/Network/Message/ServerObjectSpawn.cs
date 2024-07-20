using System.IO;
using Shared.Network;
using WorldServer.Game.Entity;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerObjectSpawn)]
    public class ServerObjectSpawn : SubPacket
    {
        public byte SpawnIndex;
        public EventObject EventObject;

        public override void Write(BinaryWriter writer)
        {
            writer.Write(this.SpawnIndex);
            writer.Write((byte)this.EventObject.Type);
            writer.Write(this.EventObject.State);
            writer.Write((byte)0);
            writer.Write(this.EventObject.ObjectId);
            writer.Write(this.EventObject.Id);
            writer.Write((uint)0);
            writer.Write((uint)0);
            writer.Write((uint)0);
            writer.Write(this.EventObject.GimmickId);
            writer.Write(this.EventObject.Scale);
            writer.Write((short)0);
            writer.Write(this.EventObject.Position.PackedOrientationShort);
            writer.Write((short)0);
            writer.Write((short)this.EventObject.PermissionInvisibility);
            writer.Write(this.EventObject.Flag);
            writer.Write((short)0);
            writer.Write(this.EventObject.HousingLink);
            writer.Write(this.EventObject.Position.Offset.X);
            writer.Write(this.EventObject.Position.Offset.Y);
            writer.Write(this.EventObject.Position.Offset.Z);
            writer.Write((short)0);
            writer.Write((short)0);
        }
    }
}