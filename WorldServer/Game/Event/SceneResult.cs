using System.Runtime.InteropServices;

namespace WorldServer.Game.Event
{
    public struct SceneResult
    {
        public uint[] Data;
        public byte errorCode;
        public byte numOfResults;
        
        public ushort param1 => (ushort)(this.errorCode + this.numOfResults << 8);
        public ushort param2 => (ushort)GetResult(0);
        public ushort param3 => (ushort)GetResult(1);
        public ushort param4 => (ushort)GetResult(2);
        


        public uint GetResult(uint index)
        {
            if (index >= this.Data.Length) return 0;
            return this.Data[index];
        }
        public SceneResult(byte errorCode, byte paramCount, uint[] data)
        {
            this.errorCode = errorCode;
            this.numOfResults = paramCount;
            this.Data = data;
        }
    }
}