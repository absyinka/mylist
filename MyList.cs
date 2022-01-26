using System;
using System.Collections.Generic;
using System.Linq;

namespace SCP // MyList<string> implementation
{
    public class MyList
    {
        // To implement ..
        string[] items;

        string[] Items { get => items; set => items = value; }

        public int Count { get; private set; }

        public int Capacity
        {
            get { return Items.Length; }
        }

        public MyList()
        {
            Items = new string[] { };
        }

        public MyList(IEnumerable<string> enumerable)
        {
            var newArray = enumerable.ToArray();

            // Ensure that the list grows in capacity with values of powers of 2
            var log = Math.Log2(newArray.Length);

            // if Math.Floor(log) is within 1e-6 of log, then Math.Floor(log) is log (for our use cases)
            var logInt = Math.Abs(Math.Floor(log) - log) <= 1e-6 ? (int)log : (int)Math.Floor(log) + 1;

            //Ensuring the capacity is maximum of four
            int capacity = Math.Max(4, (int)Math.Pow(2, logInt));

            Items = new string[capacity];

            Array.ConstrainedCopy(newArray, 0, Items, 0, newArray.Length);
            
            Count = newArray.Length;
        }

        public MyList(int capacity)
        {
            Items = new string[capacity];
        }

        public string this[int index]
        {
            get
            {
                CheckBoundaries(index);
                return Items[index];
            }
            set
            {
                if (index == Count)
                {
                    Add(value);
                    return;
                }
                CheckBoundaries(index);
                Items[index] = value;
            }
        }

        public void Add(string item)
        {
            EnsureCapacity();

            Items[Count++] = item;
        }

        public void Clear()
        {
            for (int i = 0; i < Count; i++)
            {
                Items[i] = default(string);
            }

            Count = 0;
        }

        public void TrimExcess()
        {
            string[] newArray = new string[Count];

            Array.Copy(Items, newArray, Count);

            Items = newArray;
        }

        public string[] ToArray()
        {
            string[] array = new string[Count];

            Array.Copy(Items, array, Count);

            return array;
        }

        public void Insert(string item)
        {
            try
            {
                Add(item);
            }
            catch (ArgumentOutOfRangeException)
            {
            }
        }

        public void Insert(int index, string item)
        {
            try
            {
                if (index > Count)
                {
                    Add(item);
                    return;
                }

                EnsureCapacity();

                Array.Copy(Items, index, Items, index + 1, Count - index);

                Items[index] = item;

                Count++;
            }
            catch (ArgumentOutOfRangeException)
            {
            }
        }

        public bool Contains(string item)
        {
            for (var i = 0; i < Count; i++)
            {
                if (item == Items[i])
                {
                    return true;
                }
            }

            return false;
        }

        public int IndexOf(string item)
        {
            for (var i = 0; i < Count; i++)
            {
                if (item == Items[i])
                {
                    return i;
                }
            }
            return -1;
        }

        public void RemoveAt(int index)
        {
            CheckBoundaries(index);

            for (var i = index; i < Count - 1; i++)
            {
                Items[i] = Items[i + 1];
            }

            Count--;
        }

        public void Remove(string item)
        {
            var index = IndexOf(item);

            if (index == -1)
            {
                return;
            }

            RemoveAt(index);
        }

        public void Reverse()
        {
            Array.Reverse(Items, 0, Count);
        }

        public void AddRange(string[] arrayItems)
        {
            // Ensure that the list grows in capacity with values of powers of 2
            var log = Math.Log2(Count + arrayItems.Length);

            // if Math.Floor(log) is within 1e-6 of log, then Math.Floor(log) is log (for our use cases)
            var logInt = Math.Abs(Math.Floor(log) - log) <= 1e-6 ? (int)log : (int)Math.Floor(log) + 1;

            int capacity = Math.Max(4, (int)Math.Pow(2, logInt));

            Array.Resize(ref items, capacity);

            Array.ConstrainedCopy(arrayItems, 0, Items, Count, arrayItems.Length);

            Count += arrayItems.Length;
        }

        public void Sort()
        {
            Array.Sort(Items, 0, Count);
        }

        public int BinarySearch(string item)
        {
            return Array.BinarySearch(Items, 0, Count, item);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is MyList list2) || list2.Count != Count || list2.Capacity != Capacity)
            {
                return false;
            }

            for (var i = 0; i < Count; i++)
            {
                if (this[i] != list2[i])
                {
                    return false;
                }
            }

            return true;
        }

        public static MyList operator +(MyList item1, MyList item2)
        {
            var listItems = new string[item1.Count + item2.Count];

            Array.Copy(item1.Items, 0, listItems, 0, item1.Count);

            Array.Copy(item2.Items, 0, listItems, item1.Count, item2.Count);

            return new MyList(listItems);
        }

        public static bool operator ==(MyList item1, MyList item2)
        {
            return item1.Equals(item2);
        }

        public static bool operator !=(MyList item1, MyList item2)
        {
            return !(item1.Equals(item2));
        }

        void EnsureCapacity()
        {
            EnsureCapacity(Count + 1);
        }

        void EnsureCapacity(int neededCapacity)
        {
            if (neededCapacity > Capacity)
            {
                int targetSize = Math.Max(4, Items.Length * 2);

                if (targetSize < neededCapacity)
                {
                    targetSize = neededCapacity;
                }

                Array.Resize(ref items, targetSize);
            }
        }

        void CheckBoundaries(int index)
        {
            if (index >= Count || index < 0)
            {
                throw new IndexOutOfRangeException();
            }
        }
    }
}

namespace GCP // MyList<T> implementation
{
    public class MyList<T>
    {
        // To implement ...

        T[] items;

        T[] Items { get => items; set => items = value; }

        public int Count { get; private set; }

        public int Capacity
        {
            get { return Items.Length; }
        }

        public MyList()
        {
            Items = new T[] { };
        }

        public MyList(IEnumerable<T> enumerable)
        {
            var newArray = enumerable.ToArray();

            // Ensure that the list grows in capacity with values of powers of 2
            var log = Math.Log2(newArray.Length);

            // if Math.Floor(log) is within 1e-6 of log, then Math.Floor(log) is log (for our use cases)
            var logInt = Math.Abs(Math.Floor(log) - log) <= 1e-6 ? (int)log : (int)Math.Floor(log) + 1;

            int capacity = Math.Max(4, (int)Math.Pow(2, logInt));

            Items = new T[capacity];

            Array.ConstrainedCopy(newArray, 0, Items, 0, newArray.Length);

            Count = newArray.Length;
        }

        public MyList(int capacity)
        {
            Items = new T[capacity];
        }

        public T this[int index]
        {
            get
            {
                CheckBoundaries(index);

                return Items[index];
            }

            set
            {
                if (index == Count)
                {
                    Add(value);
                    return;
                }

                CheckBoundaries(index);

                Items[index] = value;
            }
        }

        public void Add(T item)
        {
            EnsureCapacity();

            Items[Count++] = item;
        }

        public void Clear()
        {
            for (int i = 0; i < Count; i++)
            {
                Items[i] = default(T);
            }

            Count = 0;
        }

        public void TrimExcess()
        {
            T[] newArray = new T[Count];

            Array.Copy(Items, newArray, Count);

            Items = newArray;
        }

        public string[] ToArray()
        {
            string[] array = new string[Count];

            Array.Copy(Items, array, Count);

            return array;
        }

        public void Insert(T item)
        {
            try
            {
                Add(item);
            }
            catch (ArgumentOutOfRangeException)
            {
            }
        }

        public void Insert(int index, T item)
        {
            try
            {
                if (index > Count)
                {
                    Add(item);
                    return;
                }

                EnsureCapacity();

                Array.Copy(Items, index, Items, index + 1, Count - index);

                Items[index] = item;

                Count++;
            }
            catch (ArgumentOutOfRangeException)
            {
            }
        }

        public bool Contains(T item)
        {
            for (var i = 0; i < Count; i++)
            {
                if (item.Equals(Items[i]))
                {
                    return true;
                }
            }

            return false;
        }

        public int IndexOf(T item)
        {
            for (var i = 0; i < Count; i++)
            {
                if (item.Equals(Items[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        public void RemoveAt(int index)
        {
            CheckBoundaries(index);

            for (var i = index; i < Count - 1; i++)
            {
                Items[i] = Items[i + 1];
            }

            Count--;
        }

        public void Remove(T item)
        {
            var index = IndexOf(item);

            if (index == -1)
            {
                return;
            }

            RemoveAt(index);
        }

        public void Reverse()
        {
            Array.Reverse(Items, 0, Count);
        }

        public void AddRange(T[] arrayItems)
        {
            // Ensure that the list grows in capacity with values of powers of 2
            var log = Math.Log2(Count + arrayItems.Length);

            // if Math.Floor(log) is within 1e-6 of log, then Math.Floor(log) is log (for our use cases)
            var logInt = Math.Abs(Math.Floor(log) - log) <= 1e-6 ? (int)log : (int)Math.Floor(log) + 1;

            int capacity = Math.Max(4, (int)Math.Pow(2, logInt));

            Array.Resize(ref items, capacity);

            Array.ConstrainedCopy(arrayItems, 0, Items, Count, arrayItems.Length);

            Count += arrayItems.Length;
        }

        public void Sort()
        {
            Array.Sort(Items, 0, Count);
        }

        public int BinarySearch(T item)
        {
            return Array.BinarySearch(Items, 0, Count, item);
        }


        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is MyList<T> list2) || list2.Count != Count || list2.Capacity != Capacity)
            {
                return false;
            }

            for (var i = 0; i < Count; i++)
            {
                if (!(this[i].Equals(list2[i])))
                {
                    return false;
                }
            }

            return true;
        }


        public static MyList<T> operator +(MyList<T> item1, MyList<T> item2)
        {
            var listItems = new T[item1.Count + item2.Count];

            Array.Copy(item1.Items, 0, listItems, 0, item1.Count);

            Array.Copy(item2.Items, 0, listItems, item1.Count, item2.Count);

            return new MyList<T>(listItems);
        }


        public static bool operator ==(MyList<T> item1, MyList<T> item2)
        {
            return item1.Equals(item2);
        }


        public static bool operator !=(MyList<T> item1, MyList<T> item2)
        {
            return item1.Equals(item2);
        }

        void EnsureCapacity()
        {
            EnsureCapacity(Count + 1);
        }

        void EnsureCapacity(int neededCapacity)
        {
            if (neededCapacity > Capacity)
            {
                int targetSize = Math.Max(4, Items.Length * 2);

                if (targetSize < neededCapacity)
                {
                    targetSize = neededCapacity;
                }

                Array.Resize(ref items, targetSize);
            }
        }

        void CheckBoundaries(int index)
        {
            if (index >= Count || index < 0)
            {
                throw new IndexOutOfRangeException();
            }
        }

    }
}
