using System.IO;
using System.Linq;
using Shared.Network;
using WorldServer.Game.Action;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerEffect)]
    public class ServerEffect : SubPacket
    {
        public Action Action;

        public override void Write(BinaryWriter writer)
        {
            writer.Write(this.Action.TargetId);
            writer.Write(this.Action.ActionId);
            writer.Write((uint)1);
            writer.Write((float)0.1);
            writer.Write((uint)0);
            writer.Write((ushort)1);
            writer.Write(this.Action.Source.Position.PackedOrientationShort);
            writer.Write((ushort)this.Action.ActionId);
            writer.Write((byte)0);
            writer.Write((byte)1);
            writer.Write((byte)0);
            writer.Write((byte)1);
            writer.Write((ushort)0);
            writer.Write((ushort)0);
            writer.Write((ushort)0);
            writer.Write((ushort)0);
            for (var i = 0; i < 8; i++)
            {
                if (this.Action.Effects.Count > 0 && i < this.Action.Effects.FirstOrDefault().Value.Count)
                {
                    var effect = this.Action.Effects.First().Value[i];
                    writer.Write((byte)effect.Type);
                    writer.Write(effect.Parameter1);
                    writer.Write(effect.Parameter2);
                    writer.Write((byte)effect.Parameter3);
                    writer.Write((byte)0);
                    writer.Write((byte)effect.Flags);
                    writer.Write((short)effect.Value);
                }
                else
                    writer.Pad(8u);
            }
            writer.Pad(6u);
            writer.Write(this.Action.Effects.FirstOrDefault().Key);
            writer.Write((uint)0);
            writer.Write((uint)0);
            
        }
    }
}