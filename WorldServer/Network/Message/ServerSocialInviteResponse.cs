using System.IO;
using Shared.Network;
using WorldServer.Game.Social;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketServerHandlerId.ServerSocialInviteResponse)]
public class ServerSocialInviteResponse : SubPacket
{
    public ulong Id;
    public SocialType SocialType;
    public byte Flags;
    public string Invitee;

    public override void Write(BinaryWriter writer)
    {
        writer.Write(Id);
        if (Version.Version.StartsWith('7'))
        {
            writer.Write((ulong)10);
        }
        writer.Pad(4u);
        writer.Pad(1u);
        writer.Pad(1u);
        writer.Write((byte)SocialType);
        writer.Write(Flags);
        writer.WriteStringLength(Invitee, 0x20);
        writer.Pad(2u);
    }
}