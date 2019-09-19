using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree2
{
    /// <summary>
    /// Used to get function values and track which values were computed
    /// </summary>
    class ImplicitSurfaceProvider
    {
        private readonly IImplicitSurface ImplicitSurface;

        private readonly List<FunctionValue> InnerValues = new List<FunctionValue>();
        private readonly List<FunctionValue> OuterValues = new List<FunctionValue>();

        public int FunctionValueCount => InnerValues.Count + OuterValues.Count;

        public ImplicitSurfaceProvider(IImplicitSurface implicitSurface)
        {
            ImplicitSurface = implicitSurface ?? throw new ArgumentNullException(nameof(implicitSurface));
        }

        public FunctionValue CreateFunctionValue(Vector3 position)
        {
            float distance = ImplicitSurface.GetApproximateSurfaceDistance(position);

            FunctionValue value = new FunctionValue(position, distance);

            if (value.IsInside)
            {
                InnerValues.Add(value);
            }
            else
            {
                OuterValues.Add(value);
            }

            return value;
        }

        public FunctionValue[] GetInnerValues()
        {
            return InnerValues.ToArray();
        }

        public FunctionValue[] GetouterValues()
        {
            return OuterValues.ToArray();
        }
    }
}
