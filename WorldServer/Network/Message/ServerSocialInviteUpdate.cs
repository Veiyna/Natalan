using System.IO;
using Shared.Network;
using WorldServer.Game.Social;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketServerHandlerId.ServerSocialInviteUpdate)]
public class ServerSocialInviteUpdate : SubPacket
{
    public ulong CharacterId;
    public uint UnixTime;
    public SocialType SocialType;
    public SocialInviteUpdateType UpdateType;
    public byte Flags = 0x01;
    public string Name;

    public override void Write(BinaryWriter writer)
    {
        if (Version.Version.StartsWith('7'))
        {
            writer.Write((ulong)10);
        }
        writer.Write(CharacterId);
        writer.Pad(8u);
        writer.Write(UnixTime);
        writer.Pad(1u);
        writer.Pad(1u);
        writer.Write((byte)SocialType);
        writer.Write((byte)0);
        writer.Write((byte)UpdateType);
        writer.Write(Flags);
        writer.WriteStringLength(Name, 0x20);
        writer.Pad(6u);
    }
}