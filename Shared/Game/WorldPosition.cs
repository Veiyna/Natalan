using System;
using System.Numerics;

namespace Shared.Game
{
    //TODO: TerritoryId and InstanceId should probably not be part of this
    public class WorldPosition
    {
        public ushort TerritoryId { get; set; }
        public uint InstanceId { get; set; }
        public Vector3 Offset { get; set; }
        public float Orientation { get; set; }

        public Vector3 PackedOffsetShort => new(0x8000 + Offset.X * 32.767f, 0x8000 + Offset.Y * 32.767f, 0x8000 + Offset.Z * 32.767f);
        public byte PackedOrientationByte => (byte)(0x80 * (Orientation + Math.PI) / Math.PI);
        public ushort PackedOrientationShort => (ushort)(0x8000 * (Orientation + Math.PI) / Math.PI);

        public WorldPosition(ushort territoryId, Vector3 offset, float orientation, uint instanceId = 0)
        {
            TerritoryId = territoryId;
            InstanceId = instanceId;
            Offset      = offset;
            Orientation = orientation;
        }
        
        public void Relocate(WorldPosition worldPosition)
        {
            Relocate(worldPosition.Offset, worldPosition.Orientation);
        }

        public void Relocate(Vector3 position, float orientation)
        {
            Offset      = position;
            Orientation = orientation;
        }
        
        public void Relocate(float orientation)
        {
            Orientation = orientation;
        }

        public bool InRadius(WorldPosition position, float radius)
        {
            float dx = Math.Abs(Offset.X - position.Offset.X);
            if (dx > radius)
                return false;

            float dy = Math.Abs(Offset.Y - position.Offset.Y);
            if (dy > radius)
                return false;

            if (dx + dy <= radius)
                return true;

            return dx * dx + dy * dy <= radius * radius;
        }
    }
}
