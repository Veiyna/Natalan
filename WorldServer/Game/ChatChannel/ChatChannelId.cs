using System.Runtime.InteropServices;

namespace WorldServer.Game.ChatChannel;

[StructLayout(LayoutKind.Explicit)]
public struct ChatChannelId
{
        [FieldOffset(0)]
        public ulong Id;
        [FieldOffset(0)]
        public uint IncrementalId;
        [FieldOffset(4)]
        public ushort ChatType;
        [FieldOffset(6)] 
        public ushort WorldId;
}