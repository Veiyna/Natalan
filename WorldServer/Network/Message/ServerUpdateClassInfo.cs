using System.IO;
using Shared.Network;
using WorldServer.Game.Entity;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketServerHandlerId.ServerUpdateClassInfo)]
public class ServerUpdateClassInfo : SubPacket
{
    public Player Player;

    public override void Write(BinaryWriter writer)
    {
        writer.Write(this.Player.Character.ClassJobId);
        writer.Write((byte)this.Player.Character.Class.Level);
        writer.Write(this.Player.Character.Class.Level);
        writer.Write((uint)this.Player.Character.Class.Level);
        writer.Write(this.Player.Character.Class.Experience);
        writer.Write(this.Player.Character.Class.Experience);
    }
}