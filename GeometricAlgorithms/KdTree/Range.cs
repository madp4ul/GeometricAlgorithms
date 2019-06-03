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

            if (offset < 0 || offset + length > array.Length)
            {
                throw new ArgumentException("Range out of range");
            }
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index > Length)
                {
                    throw new IndexOutOfRangeException("Index was out of valid range.");
                }

                return Array[Offset + index];
            }
            set
            {
                if (index < 0 || index > Length)
                {
                    throw new IndexOutOfRangeException("Index was out of valid range.");
                }

                Array[Offset + index] = value;
            }
        }

        public Range<T> GetRange(int offset, int length)
        {
            return new Range<T>(Array, this.Offset + offset, length);
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

        public void Sort(IComparer<T> comparer)
        {
            System.Array.Sort(this.Array, Offset, Length, comparer);
        }



        /// <summary>
        /// Nth_element made with Quick select algorithm. Custom comparer. nthToSeek is zero base index
        /// http://blog.teamleadnet.com/2012/07/quick-select-algorithm-find-kth-element.html
        /// This does two things if you use it to array with index n:
        /// 1. item in index n is same as it would be if array was sorted and
        /// 2. Items before index n are <= or >= and items after n are >= or <= based on your sorting method
        /// </summary>
        /// <param name="nthToSeek">index of n</param>
        /// <param name="comparison"></param>
        public void NthElement(int nthToSeek, Comparison<T> comparison)
        {
            if (nthToSeek < 0 || nthToSeek > Length)
            {
                throw new ArgumentException("N out of range");
            }

            //Offset parameter into the represented range
            nthToSeek += Offset;

            int from = Offset;
            int to = Offset + Length;

            // if from == to we reached the kth element
            while (from < to)
            {
                int r = from, w = to;
                T mid = Array[(r + w) / 2];

                // stop if the reader and writer meets
                while (r < w)
                {
                    if (comparison(Array[r], mid) > -1)
                    { // put the large values at the end
                        T tmp = Array[w];
                        Array[w] = Array[r];
                        Array[r] = tmp;
                        w--;
                    }
                    else
                    { // the value is smaller than the pivot, skip
                        r++;
                    }
                }

                // if we stepped up (r++) we need to step one down
                if (comparison(Array[r], mid) > 0)
                {
                    r--;
                }

                // the r pointer is on the end of the first k elements
                if (nthToSeek <= r)
                {
                    to = r;
                }
                else
                {
                    from = r + 1;
                }
            }
        }
    }
}
