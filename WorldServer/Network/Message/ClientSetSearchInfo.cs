using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketClientHandlerId.ClientSetSearchInfo)]
    
public class ClientSetSearchInfo : SubPacket
{
        
    public ulong StatusMask { get; private set; }
    public byte Language { get; private set; }
        
    public string SearchComment { get; private set; }
    public override void Read(BinaryReader reader)
    {
        StatusMask = reader.ReadUInt64();
        reader.Skip(9u);
        Language = reader.ReadByte();
        SearchComment = reader.ReadStringLength(193);
    }
}