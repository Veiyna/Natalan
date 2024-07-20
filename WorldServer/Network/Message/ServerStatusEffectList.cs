using System.IO;
using Shared.Network;
using WorldServer.Game.Entity;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerStatusEffectList)]
    public class ServerStatusEffectList : SubPacket
    {
        public Character Character;

        public override void Write(BinaryWriter writer)
        {
            writer.Write(this.Character.ClassJobId);
            writer.Write(this.Character.Level);
            writer.Write((ushort)this.Character.Level);
            writer.Write(this.Character.HP);
            writer.Write(this.Character.MaxHP);
            writer.Write((ushort)this.Character.MP);
            writer.Write((ushort)this.Character.MaxMP);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write((ushort)0);
            for (var i = 0; i < 30; i++)
            {
                if (i < Character.StatusEffects.Count)
                {
                    var effect = this.Character.StatusEffects[i];
                    writer.Write((ushort)effect.StatusId);
                    writer.Write(effect.Param);
                    writer.Write(effect.TimeLeft);
                    writer.Write(effect.Source.Id);
                }
                else
                    writer.Pad(12u);
            }
            writer.Pad(4u);
            
            
        }
    }
}