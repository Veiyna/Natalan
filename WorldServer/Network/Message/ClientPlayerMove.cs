using System.IO;
using System.Numerics;
using Shared.Network;
using WorldServer.Game.Entity.Enums;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketClientHandlerId.ClientPlayerMove, false)]
    public class ClientPlayerMove : SubPacket
    {
        public float Orientation { get; private set; }
        public Vector3 Position { get; private set; }
        public Vector3 Position2 { get; private set; }  // always seems to mirror the first position

        public MoveType AnimationType;
        public MoveState AnimationState;
        public MoveType ClientAnimationType;
        public byte HeadPosition;

        public override void Read(BinaryReader reader)
        {
            Orientation = reader.ReadSingle();
            AnimationType = (MoveType)reader.ReadByte();
            this.AnimationState = (MoveState)reader.ReadByte();
            this.ClientAnimationType = (MoveType)reader.ReadByte();
            this.HeadPosition = reader.ReadByte();
            Position  = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            reader.ReadUInt32();
        }
    }
}
