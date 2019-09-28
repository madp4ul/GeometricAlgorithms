using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Domain
{
    public class Triangle : IEnumerable<int>
    {
        public readonly int Index0;
        public readonly int Index1;
        public readonly int Index2;

        public Triangle(int index0, int index1, int index2)
        {
            Index0 = index0;
            Index1 = index1;
            Index2 = index2;
        }

        public IEnumerator<int> GetEnumerator()
        {
            yield return Index0;
            yield return Index1;
            yield return Index2;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
