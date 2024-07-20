using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketServerHandlerId.ServerExamineSearchComment)]
    
public class ServerExamineSearchComment : SubPacket
{
    public uint ActorId;

    public string SearchComment;
    public override void Write(BinaryWriter writer)
    {
        writer.Write(ActorId);
        writer.WriteStringLength(this.SearchComment, 196);
            
    }
}