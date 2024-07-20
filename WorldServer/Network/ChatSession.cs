using Shared;
using Shared.Network;
using WorldServer.Game.Entity;

namespace WorldServer.Network
{
    [Session(ConnectionChannel.Chat)]
    public class ChatSession : Session
    {
        public Player Player { get; set; }
        public ChatSession()
        {
            oodle = new Oodle();
        }
        public override void Send(SubPacket subPacket)
        {
            Send(0u, 0u, subPacket);
        }

        public override SubPacketClientHandlerId ClientOpcodeToHandler(ushort opcode)
        {
            var sharedOpcode = PacketManager.SharedOpcodesClientChat.GetValueOrNull(opcode);
            if (sharedOpcode != null)
                return sharedOpcode.Value;

            return SubPacketClientHandlerId.None;
        }

        public override ushort ServerHandlerToOpcode(SubPacketServerHandlerId handlerId)
        {
            var sharedOpcode = PacketManager.SharedOpcodesServerChat.GetValueOrNull(handlerId);
            if (sharedOpcode != null)
                return sharedOpcode.Value;
            return 0;
        }
    }
}
