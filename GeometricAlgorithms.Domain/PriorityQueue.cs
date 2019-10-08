using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Domain
{
    /// <summary>
    /// Returns smallest value first
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PriorityQueue<T> where T : IComparable<T>
    {
        private readonly List<T> Data;

        public int Count => Data.Count;

        public PriorityQueue()
        {
            Data = new List<T>();
        }

        public PriorityQueue(int capacity)
        {
            Data = new List<T>(capacity);
        }

        public void Enqueue(T item)
        {
            Data.Add(item);
            int childIndex = Data.Count - 1; //start at end
            while (childIndex > 0)
            {
                int parentIndex = (childIndex - 1) / 2;
                if (Data[childIndex].CompareTo(Data[parentIndex]) >= 0)
                {
                    return; // child item is larger than (or equal) parent so we're done
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

        public List<T> RemoveWhere(Predicate<T> predicate)
        {
            var newData = new List<T>(capacity: Data.Capacity);
            var removed = new List<T>();

            foreach (var item in Data)
            {
                if (predicate(item))
                {
                    removed.Add(item);
                }
                else
                {
                    newData.Add(item);
                }
            }

            Data.Clear();

            //TODO can you maintain the heap if you just remove the items?
            foreach (var item in newData)
            {
                Enqueue(item);
            }

            return removed;
        }

        /// <summary>
        /// Get elements in queue. They are in a min heap and
        /// not strictly ordered
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetMinHeap()
        {
            return Data.AsEnumerable();
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
            int lastIndex = Data.Count - 1;
            for (int parentIndex = 0; parentIndex < Data.Count; parentIndex++)
            {
                int leftChildIndex = 2 * parentIndex + 1;
                int rightChildIndex = 2 * parentIndex + 2;

                if (leftChildIndex <= lastIndex && Data[parentIndex].CompareTo(Data[leftChildIndex]) > 0)
                    return false; // if lc exists and it's greater than parent then bad.
                if (rightChildIndex <= lastIndex && Data[parentIndex].CompareTo(Data[rightChildIndex]) > 0)
                    return false; // check the right child too.
            }
            return true; // passed all checks
        }
    }
}
