using System.Collections.Concurrent;

namespace LevelUpQrCodeListenerLibrary.DataStructures
{
    internal class FixedSizeQueue<T> : ConcurrentQueue<T>
    {
        private readonly object syncObject = new object();

        public int Size { get; private set; }

        public FixedSizeQueue(int size)
        {
            Size = size;
        }

        public new void Enqueue(T obj)
        {
            base.Enqueue(obj);
            lock (syncObject)
            {
                while (base.Count > Size)
                {
                    T outObj;
                    base.TryDequeue(out outObj);
                }
            }
        }

        public string ConvertToString() => string.Join("", this.ToArray());
    }
}
