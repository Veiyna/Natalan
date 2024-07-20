using System.IO;
using System.Numerics;
using Shared.Game;
using Shared.Network;
using WorldServer.Game.Entity;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerActorMove, false)]
    public class ServerActorMove : SubPacket
    {
        public WorldPosition Position;
        public AnimationData AnimationData;

        public override void Write(BinaryWriter writer)
        {
            writer.Write(AnimationData.HeadPosition);
            writer.Write(Position.PackedOrientationByte);
            writer.Write((byte)AnimationData.AnimationType);
            writer.Write((byte)AnimationData.AnimationState);
            writer.Write((byte)AnimationData.AnimationSpeed);
            writer.Write(AnimationData.UnknownRotation);
            /*writer.Write((byte)0x3C);
            //writer.Write((byte)0x5A);
            writer.Write((byte)0x0);*/
            Vector3 packedOffset = Position.PackedOffsetShort;
            writer.Write((ushort)packedOffset.X);
            writer.Write((ushort)packedOffset.Y);
            writer.Write((ushort)packedOffset.Z);

            writer.Write(0u);
        }
    }
}
