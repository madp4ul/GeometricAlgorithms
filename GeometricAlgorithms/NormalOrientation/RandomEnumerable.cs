using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.NormalOrientation
{
    class RandomEnumerable : IEnumerable<int>
    {
        private readonly RandomEnumerator RandomEnumerator;

        public RandomEnumerable(int maxValue)
        {
            RandomEnumerator = new RandomEnumerator(maxValue);
        }

        public IEnumerator<int> GetEnumerator()
        {
            return RandomEnumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    class RandomEnumerator : IEnumerator<int>
    {
        public readonly int MaxValue;

        private Random Factory = new Random();

        public RandomEnumerator(int maxValue)
        {
            MaxValue = maxValue;
        }

        public int Current { get; private set; }

        object IEnumerator.Current => Current;

        public void Dispose()
        {

        }

        public bool MoveNext()
        {
            Current = Factory.Next(MaxValue);
            return true;
        }

        public void Reset()
        {
            Factory = new Random();
        }
    }
}
