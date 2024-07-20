using System.IO;
using Shared.Network;
using Action = WorldServer.Game.Action.Action;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerActorCast)]
    public class ServerActorCast : SubPacket
    {
        public Action Action;

        public override void Write(BinaryWriter writer)
        {
            writer.Write((ushort)this.Action.ActionId);
            writer.Write((byte)this.Action.Type);
            writer.Pad(1u);
            writer.Write(this.Action.AdditionalId);
            writer.Write(this.Action.CastTimeMs / 1000f);
            writer.Write(this.Action.TargetId);
            writer.Write(this.Action.Position.PackedOrientationShort);
            writer.Pad(2u);
            writer.Write((uint)0);
            writer.Write(this.Action.Position.Offset.X);
            writer.Write(this.Action.Position.Offset.Y);
            writer.Write(this.Action.Position.Offset.Z);
            writer.Pad(2u);
        }
    }
}