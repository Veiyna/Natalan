using System.IO;
using System.Linq;
using Shared.Network;
using Action = WorldServer.Game.Action.Action;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketServerHandlerId.ServerAoeEffect8)]
public class ServerAoeEffect8 : SubPacket
{
    public Action Action;

    public override void Write(BinaryWriter writer)
    {
        var effects = this.Action.Effects.Values.ToList();
        var PlayerCount = 8;
        writer.Write(this.Action.TargetId);
        writer.Write(this.Action.ActionId);
        writer.Write((uint)1);
        writer.Write((float)0.1);
        writer.Write((uint)0);
        writer.Write((ushort)0);
        writer.Write(this.Action.Source.Position.PackedOrientationShort);
        writer.Write((ushort)this.Action.ActionId);
        writer.Write((byte)0);
        writer.Write((byte)1);
        writer.Write((byte)0);
        writer.Write((byte)effects.Count);
        writer.Write((ushort)0);
        writer.Write((ushort)0);
        writer.Write((ushort)0);
        writer.Write((ushort)0);
        for (int playercount = 0; playercount < PlayerCount; playercount++)
        {
            if (playercount < this.Action.Effects.Count)
            {
                var effectlist = effects[playercount];
                for (int i = 0; i < 8; i++)
                {
                    if (i < effectlist.Count)
                    {
                        var effect = effects[playercount][i];
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
            }
            else
            {
                writer.Pad((uint)8*8);
            }
        }
            

        writer.Pad(6u);
        for (int i = 0; i < PlayerCount; i++)
        {
            if (i < effects.Count)
            {
                writer.Write((ulong)effects[i].First().Target.Id);
            }
            else
            {
                writer.Write((ulong)0);
            }
        }
            
        writer.Write((ushort)0x7FFF);
        writer.Write((ushort)0x7FFF);
        writer.Write((ushort)0x7FFF);
        writer.Write((ushort)0);
        writer.Write((ushort)0);
        writer.Write((ushort)0);
            
    }
}