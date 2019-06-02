using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.KdTree
{
    /// <summary>
    /// Provides partial access to an array. Can be further restricted
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class Range<T> : IEnumerable<T>
    {
        private T[] Array { get; set; }
        private int Offset { get; set; }
        public int Length { get; set; }

        private Range(T[] array, int offset, int length)
        {
            Array = array ?? throw new ArgumentNullException(nameof(array));
            Offset = offset;
            Length = length;

            if (Offset + Length > Array.Length)
            {
                throw new ArgumentException("Range out of range");
            }
        }

        public T this[int index]
        {
            get
            {
                if (index > Length)
                {
                    throw new IndexOutOfRangeException("Index was out of valid range.");
                }

                return Array[Offset + index];
            }
            set
            {
                if (index > Length)
                {
                    throw new IndexOutOfRangeException("Index was out of valid range.");
                }

                Array[Offset + index] = value;
            }
        }

        public Range<T> GetRange(int offset, int length)
        {
            if (offset + length > this.Length)
            {
                throw new ArgumentException("Range out of range");
            }

            return new Range<T>(Array, this.Offset + offset, length);
        }

        public void Sort(IComparer<T> comparer)
        {
            System.Array.Sort(this.Array, Offset, Length, comparer);
        }

        public static Range<T> FromArray(T[] array, int offset, int length)
        {
            return new Range<T>(array, offset, length);
        }

        public IEnumerator<T> GetEnumerator()
        {
            int lastIndex = Offset + Length;

            for (int currentIndex = Offset; currentIndex < lastIndex; currentIndex++)
            {
                yield return Array[currentIndex];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
