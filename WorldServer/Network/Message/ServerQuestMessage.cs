using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketServerHandlerId.ServerQuestMessage)]
public class ServerQuestMessage : SubPacket
{

    public uint QuestId;
    public byte MsgId;
    public byte Type;
    public uint Var1;
    public uint Var2;
    public override void Write(BinaryWriter writer)
    {
        writer.Write(this.QuestId);
        writer.Write(this.MsgId);
        writer.Write(this.Type);
        writer.Pad(2u);
        writer.Write(this.Var1);
        writer.Write(this.Var2);
    }
}