using System.IO;
using Shared.Network;
using WorldServer.Game.StatusEffect;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerEffectResult)]
    public class ServerEffectResult : SubPacket
    {
        public byte index;
        public StatusEffect effect;

        public override void Write(BinaryWriter writer)
        {
            writer.Write((uint)1);
            writer.Write((uint)0);
            writer.Write(this.effect.Target.Id);
            writer.Write(this.effect.Target.HP);
            writer.Write(this.effect.Target.MaxHP);
            writer.Write((ushort)this.effect.Target.MP);
            writer.Write((byte)0);
            writer.Write(this.effect.Target.ToPlayer?.Character.ClassJobId ?? 0);
            writer.Write((byte)0);
            writer.Write((byte)1);
            writer.Write((ushort)0);
            for (var i = 0; i < 4; i++)
            {
                if (i < 1)
                {
                    writer.Write(index);
                    writer.Write((byte)0);
                    writer.Write((ushort)this.effect.StatusId);
                    writer.Write(effect.Param);
                    writer.Write((ushort)0);
                    writer.Write(effect.TimeLeft);
                    writer.Write(effect.Source.Id);
                }
                else
                    writer.Pad(16u);
            }
        }
    }
}