  using System.IO;
using System.Numerics;
using Shared.Game;
using Shared.Network;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerActorSetPos)]
    public class ServerActorSetPos : SubPacket
    {
        public WorldPosition Position;

        public override void Write(BinaryWriter writer)
        {
            writer.Write(this.Position.PackedOrientationShort);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write((uint)0);
            Vector3 packedOffset = Position.Offset;
            writer.Write(packedOffset.X);
            writer.Write(packedOffset.Y);
            writer.Write(packedOffset.Z);
            writer.Write((uint)0);
            
        }
    }
}