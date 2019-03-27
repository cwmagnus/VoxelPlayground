using System.Collections.Generic;

namespace Management.Registry
{
    public class Registry<T>
    {
        private ushort nextIndex;
        private List<T> registry = new List<T>();

        public void Register(T item)
        {
            registry.Add(item);

            nextIndex++;
        }

        public T GetItem(ushort id)
        {
            return registry[id];
        }

        public ushort GetNextIndex()
        {
            return nextIndex;
        }
    }
}
