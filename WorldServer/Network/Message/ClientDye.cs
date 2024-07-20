using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketClientHandlerId.ClientDye)]
public class ClientDye : SubPacket
{
    public uint ContainerType { get; private set; }
    public uint Slot { get; private set; }
        
    public uint DyeContainer1 { get; private set; }
    public uint DyeContainer2 { get; private set; }
    public ushort DyeSlot1 { get; private set; }
    public ushort DyeSlot2 { get; private set; }
        
    public override void Read(BinaryReader reader)
    {
        ContainerType = reader.ReadUInt32();
        Slot = reader.ReadUInt32();
        DyeContainer1 = reader.ReadUInt32();
        DyeContainer2 = reader.ReadUInt32();
        DyeSlot1 = reader.ReadUInt16();
        DyeSlot2 = reader.ReadUInt16();
    }
}