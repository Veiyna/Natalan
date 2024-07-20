using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketClientHandlerId.ClientCFRegisterDuty)]
public class ClientCFRegisterDuty : SubPacket
{
    public ushort[] Duties = new ushort[5];
    public byte UndersizedFlag;

    public override void Read(BinaryReader reader)
    {
        reader.Skip(9u);
        this.UndersizedFlag = reader.ReadByte();
        reader.Skip(16u);
        for (uint i = 0u; i < 5u; i++)
            this.Duties[i] = reader.ReadUInt16();
    }
}