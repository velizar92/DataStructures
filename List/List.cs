using System.Collections;

namespace List
{
    public class List<T> : IAbstractList<T>
    {
        private T[] _items;
        private const int DEFAULT_CAPACITY = 4;

        public int Count { get; set; }

        public T this[int index]
        {
            get
            {
                ValidateIndex(index);
                return _items[index];
            }
            set
            {
                ValidateIndex(index);
                _items[index] = value;
            }
        
        }

        public List()
            : this(DEFAULT_CAPACITY)
        {}

        public List(int capacity)
        {
            if (capacity < 0)
            {
                throw new IndexOutOfRangeException("Invalid argument for capacity!");
            }

            _items = new T[capacity];
        }

        public void Add(T item)
        {
            GrowIfNecessary();

            _items[Count] = item;
            Count++;
        }


        public void Insert(int index, T item)
        {
            ValidateIndex(index);
            GrowIfNecessary();

            for (int i = Count; i > index; i--)
            {
                _items[i] = _items[i - 1];         
            }

            _items[index] = item;
            Count++;
        }


        public bool Contains(T item)
        {
            return IndexOf(item) != -1 ? true : false;
        }


        public int IndexOf(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (item.Equals(_items[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        public bool Remove(T item)
        {
            int index = IndexOf(item);

            if (index == -1)
            {
                return false;
            }

            RemoveAt(index);
            return true;
        }

        public void RemoveAt(int index)
        {
            ValidateIndex(index);

            for (int i = index; i < Count - 1; i++)
            {
                _items[i] = _items[i + 1];
            }

            _items[Count - 1] = default;
            Count--;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return _items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }


        private void GrowIfNecessary()
        {
            if (_items.Length == Count)
            {
                Grow();
            }
        }

        private void Grow()
        {
            var newArray = new T[Count * 2];
            Array.Copy(_items, newArray, _items.Length);
            _items = newArray;
        }


        private void ValidateIndex(int index)
        {
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException(nameof(index));
            }
        }

        
    }
}
