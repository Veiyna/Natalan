using System;
using WorldServer.Game.Entity;
using WorldServer.Game.Map;

namespace WorldServer.Network
{
    public class GmCommandParameters
    {
        public uint[] Parameters { get; }
        public uint TargetActorId { get; }
        public string TargetActorName { get; }
        
        public Player Target { get; }

        public GmCommandParameters(byte[] parameterBuffer, uint targetActorId, string targetActorName)
        {
            TargetActorId   = targetActorId;
            TargetActorName = targetActorName;
            
            
            if (TargetActorId != 0u)
            {
                Target = MapManager.FindPlayer(TargetActorId);
            }
            else if (TargetActorName != string.Empty)
            {
                Target = MapManager.FindPlayer(TargetActorName);
            }
            


            if (parameterBuffer.Length % sizeof(uint) != 0)
                return;

            Parameters = new uint[parameterBuffer.Length / sizeof(uint)];
            Buffer.BlockCopy(parameterBuffer, 0, Parameters, 0, parameterBuffer.Length);
        }
    }
}
