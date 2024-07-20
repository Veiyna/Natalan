using System.Runtime.InteropServices;
using WorldServer.Game.Housing.Enums;

namespace WorldServer.Game.Housing;

[StructLayout(LayoutKind.Sequential)]
public struct LandFlagSet
{
    public LandIdent landIdent;
    public LandFlags landFlags;
    public uint unknown;
}