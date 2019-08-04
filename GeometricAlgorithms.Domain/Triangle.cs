using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Domain
{
    public class Triangle : IFace
    {
        public readonly int Index1;
        public readonly int Index2;
        public readonly int Index3;

        public Triangle(int index1, int index2, int index3)
        {
            Index1 = index1;
            Index2 = index2;
            Index3 = index3;
        }

        public IEnumerator<int> GetEnumerator()
        {
            yield return Index1;
            yield return Index2;
            yield return Index3;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
