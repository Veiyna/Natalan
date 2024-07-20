using System.Collections.Generic;
using WorldServer.Manager;

namespace WorldServer.Network.Connection;

public static class GameConnectionManager
{
    private static Dictionary<uint, GameConnection> Connections { get; set; } = new();
    
    public static GameConnection GetConnection(uint id)
    {
        if (!Connections.ContainsKey(id))
        {
            var connection = new GameConnection();
            Connections.Add(id, connection);
            return connection;
        }

        return Connections[id];
    }


}