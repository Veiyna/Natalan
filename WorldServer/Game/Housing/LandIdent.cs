using System.Runtime.InteropServices;

namespace WorldServer.Game.Housing;

[StructLayout(LayoutKind.Sequential)]
public struct LandIdent
{
    public ushort LandId;
    public ushort WardNumber;
    public ushort TerritoryId;
    public ushort WorldId;
}