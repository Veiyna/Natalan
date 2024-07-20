using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketClientHandlerId.ClientExamineSearchComment)]
public class ClientExamineSearchComment : SubPacket
{

        
    public uint ActorId { get; private set; }

    public override void Read(BinaryReader reader)
    {
        ActorId = reader.ReadUInt32();
    }
}