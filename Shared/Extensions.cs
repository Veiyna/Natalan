using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Lumina.Excel;

namespace Shared
{
    public static class Extensions
    {
        public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0)
                return min;
            return val.CompareTo(max) > 0 ? max : val;
        }

        public static bool InRange<T>(this T value, T min, T max) where T : IComparable<T>
        {
            if (value.CompareTo(min) < 0)
                return false;
            return value.CompareTo(max) <= 0;
        }

        public static IEnumerable<T> DequeueMultiple<T>(this ConcurrentQueue<T> queue, uint size)
        {
            for (uint i = 0u; i < size && queue.Count > 0; i++)
                if (queue.TryDequeue(out T value))
                    yield return value;
        }

        public static void Clear<T>(this ConcurrentQueue<T> queue)
        {
            while (queue.Count > 0)
                queue.TryDequeue(out var value);
        }

        public static T[] RangeSubset<T>(this T[] array, int startIndex, int length)
        {
            var subset = new T[length];
            Array.Copy(array, startIndex, subset, 0, length);
            return subset;
        }

        public static byte[] ToArray(this BitArray bitArray)
        {
            byte[] buffer = new byte[bitArray.Length / 8];
            bitArray.CopyTo(buffer, 0);
            return buffer;
        }
        
        public static int BytesNeeded<T>(this ExcelSheet<T> sheet) where T : ExcelRow
        {
            return (int)(sheet.RowCount + 7) >> 3;
        }
        
        public static TValue? GetValueOrNull<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
            where TValue : struct
        {
            if (dict.TryGetValue(key, out var value))
            {
                return value;
            }
            return null;
        }
        
        public static TV GetValue<TK, TV>(this IDictionary<TK, TV> dict, TK key, TV defaultValue = default)
        {
            return dict.TryGetValue(key, out var value) ? value : defaultValue;
        }
        
        public static bool ContentEquals<T,T2>(this T model, T2 model2)
        {
            long changes = 0;
            foreach (var propertyInfo in typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                var secondPropertyInfo = typeof(T2).GetProperty(propertyInfo.Name, BindingFlags.Instance | BindingFlags.Public);
                if (secondPropertyInfo != null)
                {
                    if (!propertyInfo.GetValue(model).Equals(secondPropertyInfo.GetValue(model2)))
                    {
                        changes++;
                    }
                }
            }
            return changes == 0;
        }
        
        public static byte[] ToByteArray<T>(this T[] array) where T : struct
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            int size = Buffer.ByteLength(array);
            byte[] byteArray = new byte[size];
            Buffer.BlockCopy(array, 0, byteArray, 0, size);
            return byteArray;
        }
        
        public static uint[] ReadUInts(this BinaryReader reader, int count)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

            var uintArray = new uint[count];
            for (var i = 0; i < count; i++)
            {
                uintArray[i] = reader.ReadUInt32();
            }

            return uintArray;
        }
        

    }
}
