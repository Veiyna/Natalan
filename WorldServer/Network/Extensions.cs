using System;
using System.Collections;

namespace WorldServer.Network
{
    public static class Extensions
    {
        public static byte[] ToArray(this BitArray bitArray)
        {
            byte[] buffer = new byte[bitArray.Length / 8];
            bitArray.CopyTo(buffer, 0);
            return buffer;
        }
        
        private static int BitsToBytes(uint bits)
        {
            return (int)(bits + 7) >> 3;
        }
        public static byte[] ToArray(this BitArray bitArray, int size)
        {
            byte[] buffer = new byte[size];
            var newArray = bitArray.ToArray();
            var LengthToCopy = Math.Min(newArray.Length, buffer.Length);
            Buffer.BlockCopy(newArray, 0, buffer, 0, LengthToCopy);
            return buffer;
        }
        
        public static void setHigh(ref byte b, byte val) {
            b = (byte)((b & 0xf) | (val << 4));
        }

        public static byte high(byte b) {
            return (byte)((b & 0xf0) >> 4);
        }

        public static void setLow(ref byte b, byte val) {
            b = (byte)((b & 0xf0) | val);
        }

        public static byte low(byte b) {
            return (byte)(b & 0xf);
        }
    }
}
