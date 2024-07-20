using System.IO;
using System.Numerics;
using Shared.Network;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketClientHandlerId.ClientActorAction, false)]
    [SubPacket(SubPacketClientHandlerId.ClientActorActionEnvironment, false)]
    public class ClientActorAction : SubPacket
    {
        public ActorActionClient Action { get; private set; }
        public uint[] Parameters { get; } = new uint[5];

        public Vector3 Position { get; private set; }

        public override void Read(BinaryReader reader)
        {
            Action = (ActorActionClient)reader.ReadUInt16();
            reader.ReadUInt16();

            for (uint i = 0u; i < 4u; i++)
                Parameters[i] = reader.ReadUInt32();
            Position  = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
        }
    }
}
