using System.IO;
using Shared.Network;
using WorldServer.Game.Entity;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerChannelChat)]
    public class ServerChannelChat : SubPacket
    {
        public ulong ChannelId;
        public Player Sender;
        public string Message;
        public override void Write(BinaryWriter writer)
        {
            writer.Write(this.ChannelId);
            if (Version.Version.StartsWith('7')) // DAWNTRAIL BLACKLIST UPDATE
            {
                writer.Write((ulong)10); // account id here
            }
            writer.Write(Sender.Character.Id);
            writer.Write(this.Sender.Character.ActorId);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.WriteStringLength(this.Sender.Character.Name, 32);
            writer.WriteStringLength(this.Message, 1024);
            writer.Write((byte)0);
            

        }
    }
}