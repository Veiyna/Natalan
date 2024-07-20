using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketServerHandlerId.ServerActorDespawn)]
public class ServerActorDespawn : SubPacket
{
    public uint SpawnId;
        
    public uint ActorId;

    public override void Write(BinaryWriter writer)
    {
        writer.Write(SpawnId);
        writer.Write(ActorId);
    }
}