using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Shared.Network
{
    public static class Extensions
    {
        public static byte[] Marshal(this object obj)
        {
            GCHandle handle = GCHandle.Alloc(obj, GCHandleType.Pinned);
            try
            {
                byte[] payload = new byte[System.Runtime.InteropServices.Marshal.SizeOf(obj.GetType())];
                System.Runtime.InteropServices.Marshal.Copy(handle.AddrOfPinnedObject(), payload, 0, payload.Length);
                return payload;
            }
            finally
            {
                handle.Free();
            }
        }

        public static T UnMarshal<T>(this byte[] data) where T : new()
        {
            GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            try
            {
                return (T)System.Runtime.InteropServices.Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            }
            finally
            {
                handle.Free();
            }
        }

        public static void Pad(this BinaryWriter writer, uint length)
        {
            writer.Write(new byte[length]);
        }

        public static void Skip(this BinaryReader reader, uint count)
        {
            reader.BaseStream.Position += count;
        }

        public static byte[] Copy(this byte[] buffer, int offset, int count)
        {
            byte[] subBuffer = new byte[count];
            Buffer.BlockCopy(buffer, offset, subBuffer, 0, count);
            return subBuffer;
        }

        public static string ReadStringLength(this BinaryReader reader, uint length, bool trim = false)
        {
            byte[] buffer = reader.ReadBytes((int)length);
            if (!trim)
                return Encoding.Latin1.GetString(buffer).TrimEnd('\0');

            // some packets don't seem to zero out string buffer, find first null and trim from there
            int index = Array.IndexOf<byte>(buffer, 0);
            return Encoding.Latin1.GetString(buffer, 0, index != -1 ? index : (int)length);
        }
        
        public static string ReadStringNull(this BinaryReader reader)
        {
            var result = new StringBuilder();
            while (true)
            {
                byte b = reader.ReadByte();
                if (0 == b)
                    break;
                result.Append((char)b);
            }
            return result.ToString();
        }

        public static void WriteStringLength(this BinaryWriter writer, string str, uint length)
        {
            writer.Write(Encoding.Latin1.GetBytes(str.PadRight((int)length, '\0')));
        }
    }
}
