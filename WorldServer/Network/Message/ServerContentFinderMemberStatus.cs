using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketServerHandlerId.ServerContentFinderMemberStatus)]
public class ServerContentFinderMemberStatus : SubPacket
{
    public ushort ContentId;
    public ushort Status;

    public byte CurrentTank;
    public byte MaxTank;
    public byte CurrentDPS;
    public byte MaxDPS;
    public byte CurrentHealer;
    public byte MaxHealer;

    public override void Write(BinaryWriter writer)
    {
        writer.Write(Status);
        writer.Pad(2u);
        writer.Write(ContentId);

        writer.Write((byte)1); // waiting for a match
        writer.Write((byte)0); // reserving server
        writer.Write((byte)0); // role waiting list number
        writer.Write((byte)0); // waiting time
        writer.Write((byte)0); // unknown
        writer.Write((byte)0); // unknown


        writer.Write(CurrentTank); // 7
        writer.Write(this.MaxTank); // max tank
        writer.Write(CurrentHealer); // 5
        writer.Write(this.MaxHealer); // max healer
        writer.Write(CurrentDPS); // 3
        writer.Write(this.MaxDPS); // max dps
        writer.Pad(6u); // unknown
    }
}