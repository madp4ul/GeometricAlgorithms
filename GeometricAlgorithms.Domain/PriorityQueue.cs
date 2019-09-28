using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Domain
{
    /// <summary>
    /// Takes smallest value first
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PriorityQueue<T> : IEnumerable<T> where T : IComparable<T>
    {
        private readonly List<T> Data = new List<T>();

        public int Count => Data.Count;

        public void Enqueue(T item)
        {
            Data.Add(item);
            int childIndex = Data.Count - 1; //start at end
            while (childIndex > 0)
            {
                int parentIndex = (childIndex - 1) / 2;
                if (Data[childIndex].CompareTo(Data[parentIndex]) >= 0)
                {
                    break; // child item is larger than (or equal) parent so we're done
                }

                T temp = Data[childIndex];
                Data[childIndex] = Data[parentIndex];
                Data[parentIndex] = temp;
                childIndex = parentIndex;
            }
        }

        public T Dequeue()
        {
            // assumes pq is not empty; up to calling code
            int lastIndex = Data.Count - 1; // last index (before removal)
            T frontItem = Data[0];   // fetch the front
            Data[0] = Data[lastIndex];
            Data.RemoveAt(lastIndex);

            lastIndex--; // last index (after removal)
            int parentIndex = 0; // parent index. start at front of pq
            while (true)
            {
                int childIndex = parentIndex * 2 + 1; // left child index of parent

                if (childIndex > lastIndex)
                {
                    break;  // no children so done
                }

                int rightChild = childIndex + 1;

                // if there is a rc (ci + 1), and it is smaller than left child, use the rc instead
                if (rightChild <= lastIndex && Data[rightChild].CompareTo(Data[childIndex]) < 0)
                {
                    childIndex = rightChild;
                }

                if (Data[parentIndex].CompareTo(Data[childIndex]) <= 0)
                {
                    break; // parent is smaller than (or equal to) smallest child so done
                }

                T temp = Data[parentIndex];
                Data[parentIndex] = Data[childIndex];
                Data[childIndex] = temp; // swap parent and child
                parentIndex = childIndex;
            }
            return frontItem;
        }

        public T Peek()
        {
            T frontItem = Data[0];
            return frontItem;
        }

        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < Data.Count; ++i)
                s += Data[i].ToString() + " ";
            s += "count = " + Data.Count;
            return s;
        }

        public bool IsConsistent()
        {
            // is the heap property true for all data?
            if (Data.Count == 0)
                return true;
            int li = Data.Count - 1; // last index
            for (int pi = 0; pi < Data.Count; ++pi)
            { // each parent index
                int lci = 2 * pi + 1; // left child index
                int rci = 2 * pi + 2; // right child index

                if (lci <= li && Data[pi].CompareTo(Data[lci]) > 0)
                    return false; // if lc exists and it's greater than parent then bad.
                if (rci <= li && Data[pi].CompareTo(Data[rci]) > 0)
                    return false; // check the right child too.
            }
            return true; // passed all checks
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
