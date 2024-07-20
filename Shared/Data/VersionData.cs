using System.Collections.Generic;
using Shared.Network;

namespace WorldServer.Data;

public class VersionData
{
    public string Version { get; set; }
    public string GameVersion { get; set; }
    public string FFXIVModule { get; set; }

    public Dictionary<string, int> DataSize;

    public Dictionary<SubPacketClientHandlerId, ushort> ClientOpcodes;
    public Dictionary<SubPacketServerHandlerId, ushort> ServerOpcodes;
    
}