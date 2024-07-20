using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketServerHandlerId.ServerHousingObjectInitialize)]
public class ServerHousingObjectInitialize : SubPacket
{
    public byte Index;
    public override void Write(BinaryWriter writer)
    {
        writer.Write(ushort.MaxValue);
        writer.Write(ushort.MaxValue);
        writer.Write(ushort.MaxValue);
        writer.Write(ushort.MaxValue);
        writer.Write(byte.MaxValue);
        writer.Write(this.Index);
        writer.Write((byte)8);
        writer.Write((byte)0);
        for (int i = 0; i < 100; i++)
        {
            writer.Write((uint)0);
            writer.Write((uint)0);
            writer.Write((float)0);
            writer.Write((float)0);
            writer.Write((float)0);
            writer.Write((float)0);
        }
        writer.Write((uint)0);
    }
        
}