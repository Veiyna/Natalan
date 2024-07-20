using System.IO;
using Shared.Network;
using WorldServer.Manager;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketServerHandlerId.ServerNoviceNetworkId)]
public class ServerNoviceNetworkId : SubPacket
{
    public override void Write(BinaryWriter writer)
    {
            writer.Write(AssetManager.NoviceNetworkChannel);
        }
        
}