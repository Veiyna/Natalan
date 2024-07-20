using System;
using System.Numerics;

namespace Shared
{
    public static class Utilities
    {
        public static float EulerToDirection(Vector3 euler)
        {
            Matrix4x4 matrix = new();

            var sinZ = (float)Math.Sin(euler.Z);
            var cosZ = (float)Math.Cos(euler.Z);
            var sinY = (float)Math.Sin(euler.Y);
            var cosY = (float)Math.Cos(euler.Y);
            var sinX = (float)Math.Sin(euler.X);
            var cosX = (float)Math.Cos(euler.X);

            matrix[0, 0] = cosZ * cosY;
            matrix[0, 1] = sinZ * cosX;
            matrix[0, 2] = sinZ * sinX + -cosZ * sinY * cosX;

            matrix[1, 0] = -sinZ * cosY;
            matrix[1, 1] = cosZ * cosX + -sinZ * sinY * sinX;
            matrix[1, 2] = cosZ * sinX + sinZ * sinY * cosX;

            matrix[2, 0] = sinY;
            matrix[2, 1] = -cosY * sinX;
            matrix[2, 2] = cosY * cosX;

            var AXIS_Z = new Vector3( 0.0f, 0.0f, 1.0f);
        
            var result = Vector3.Transform(AXIS_Z, matrix);

            var squared = result.Z * result.Z + result.X * result.X;
            var v1 = 0.0f;
            var v2 = 0.0f;

            if (squared > 0.00000011920929f)
            {
                var ret = Math.Sqrt(squared);
                ret = -(squared * ret * ret - 1.0f) * (0.5f * ret) + ret;
                ret = -(squared * ret * ret - 1.0f) * (0.5f * ret) + ret;
                v1 = (float)(result.Z * (-(squared * ret * ret - 1.0f) * (0.5f * ret) + ret));
                v2 = (float)(result.X * (-(squared * ret * ret - 1.0f) * (0.5f * ret) + ret));
            }

            return (float)Math.Atan2(v2, v1);
        }
        
        public static float CalcAngFrom(float x, float y, float x1, float y1)
        {
            float dx = x - x1;
            float dy = y - y1;
            if (dy != 0.0f)
            {
                return (float)Math.Atan2(dy, dx);
            }

            return 0.0f;
        }
    }
}