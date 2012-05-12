using System.Collections.Generic;
using System.Linq;

namespace catan
{
    class Heap<T> : List<T>
    {
        public Heap(int capacity)
            : base(capacity)
        { }

        public void Add(int numberOfElements, params object[] args)
        {
            for (int i = 0; i < numberOfElements; ++i)
                Add((T)System.Activator.CreateInstance(typeof(T), args));
        }

        public T TakeAway(int index)
        {
            T item = this.ElementAt(index);
            RemoveAt(index);
            return item;
        }
    }

}