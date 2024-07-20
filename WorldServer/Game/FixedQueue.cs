
using System.Collections.Generic;

namespace WorldServer.Game
{
    public class FixedQueue<T> : Queue<T>
    {
        public uint Size { get; }

        public FixedQueue(uint size)
        {
            Size = size;
            while (Count < Size)
                Enqueue(default);
        }

        public new void Enqueue(T obj)
        {
            base.Enqueue(obj);
            while (Count > Size)
                Dequeue();
        }
    }
}
