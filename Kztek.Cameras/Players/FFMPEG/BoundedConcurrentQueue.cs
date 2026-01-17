using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Kztek.Cameras.Players.FFMPEG
{
    public class BoundedConcurrentQueue<T>
    {
        private readonly ConcurrentQueue<T> _queue = new ConcurrentQueue<T>();
        private readonly int _capacity;
        private readonly object _lock = new object();

        public BoundedConcurrentQueue(int capacity)
        {
            _capacity = capacity;
        }

        public void Enqueue(T item, Action<T>? onDrop = null)
        {
            //lock (_lock)
            {
                if (_queue.Count >= _capacity)
                {
                    _queue.TryDequeue(out T removed);
                    if (removed != null && onDrop != null)
                        onDrop?.Invoke(removed);
                }
                _queue.Enqueue(item);
            }
        }

        public bool TryDequeue(out T item)
        {
            //lock (_lock)
            {
                if (_queue.Count > 0)
                {
                    _queue.TryDequeue(out item);
                    return item != null;
                }

                item = default!;
                return false;
            }
        }

        public T[] ToArray()
        {
            return _queue.ToArray();
        }

        public int Count
        {
            get { lock (_lock) { return _queue.Count; } }
        }
        public void Clear()
        {
            lock (_lock)
            {
                while (_queue.TryDequeue(out _)) ;
            }
        }
    }
}
