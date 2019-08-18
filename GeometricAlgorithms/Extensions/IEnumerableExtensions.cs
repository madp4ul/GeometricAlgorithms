using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Extensions
{
    static class IEnumerableExtensions
    {
        public static int? FirstIndexOrDefault<T>(this IEnumerable<T> enumerable, Predicate<T> predicate)
        {
            int index = 0;
            foreach (var item in enumerable)
            {
                if (predicate(item))
                {
                    return index;
                }

                index++;
            }

            return null;
        }
    }
}
