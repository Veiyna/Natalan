using System.IO;
using System.Numerics;
using Shared.Network;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketClientHandlerId.ClientSkillAoE)]
    
public class ClientSkillAoE : SubPacket
{

    public byte Type { get; private set; }
    public uint ActionId { get; private set; }
    public ushort Sequence { get; private set; }
    public Vector3 Position { get; private set; }
    public override void Read(BinaryReader reader)
    {
        reader.Skip(1u);
        Type = reader.ReadByte();
        reader.Skip(2u);
        ActionId = reader.ReadUInt32();
        Sequence = reader.ReadUInt16();
        reader.Skip(6u);
        Position  = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
    }
}