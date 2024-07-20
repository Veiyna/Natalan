using System.IO;
using Shared.Network;
using WorldServer.Manager;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketServerHandlerId.ServerWorldVisit)]
public class ServerWorldVisit : SubPacket
{
    public override void Write(BinaryWriter writer)
    {
        var realms = AssetManager.realmInfoStore;
        for (int i = 0; i < 16; i++)
        {
            if (i < realms.Count)
            {
                writer.Write(realms[i].Id);
                writer.Write((ushort)1);
            }
            else
            {
                writer.Pad(4u);
            }
        }
    }
}