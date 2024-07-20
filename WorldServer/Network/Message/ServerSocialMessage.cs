using System.IO;
using Shared.Network;
using WorldServer.Game;
using WorldServer.Game.Social;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketServerHandlerId.ServerSocialMessage)]
public class ServerSocialMessage : SubPacket
{
    public ulong Id;
    public LogMessage LogMessage;
    public SocialType SocialType;
    public byte Byte1;
    public byte Byte2;
    public string Name;

    public override void Write(BinaryWriter writer)
    {
        writer.Write(Id);
        if (Version.Version.StartsWith('7'))
        {
            writer.Write((ulong)10);
        }
        writer.Write((uint)LogMessage);
        writer.Pad(2u);

        writer.Write((byte)SocialType);
        writer.Write(Byte1);
        writer.Write(Byte2);

        writer.WriteStringLength(Name, 0x20);
        writer.Pad(1u);
            
    }
}