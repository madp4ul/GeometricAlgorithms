using GeometricAlgorithms.Domain;
using GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.Approximation;
using GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.Edges;
using GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.FunctionValues;
using GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.Sides;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.Cubes
{
    class CubeInsideEdges
    {
        private readonly Edge[] Edges = new Edge[6];

        public CubeInsideEdges(RefiningApproximation approximation, ImplicitSurfaceProvider implicitSurface, CubeOutsides outsideContainer)
        {
            var middleFunctionValue = CreateMiddleFunctionValue(implicitSurface, outsideContainer);

            for (int i = 0; i < (int)Dimension.Count; i++)
            {
                Dimension current = (Dimension)i;
                var edgeDirectionsFromCubeCenter = Dimensions.All.Where(d => d != current).ToArray();

                FunctionValue minValue = GetSideMiddleValue(outsideContainer, new SideOrientation(current, false));
                FunctionValue maxValue = GetSideMiddleValue(outsideContainer, new SideOrientation(current, true));

                Edge minEdge = new Edge(approximation, implicitSurface, edgeDirectionsFromCubeCenter, minValue, middleFunctionValue);
                Edge maxEdge = new Edge(approximation, implicitSurface, edgeDirectionsFromCubeCenter, middleFunctionValue, maxValue);

                this[current, false] = minEdge;
                this[current, true] = maxEdge;
            }
        }

        private FunctionValue GetSideMiddleValue(CubeOutsides outsideContainer, SideOrientation orientation)
        {
            return outsideContainer[orientation].Children[0, 0].Edges[0, 1].MaxValue;
        }

        private FunctionValue CreateMiddleFunctionValue(ImplicitSurfaceProvider implicitSurface, CubeOutsides outsideContainer)
        {
            Vector3 middleX = GetSideMiddleValue(outsideContainer, new SideOrientation(SideIndex.minX)).Position;
            Vector3 middleY = GetSideMiddleValue(outsideContainer, new SideOrientation(SideIndex.minY)).Position;

            Vector3 middle = new Vector3(middleY.X, middleX.Y, middleX.Z);

            return implicitSurface.CreateFunctionValue(middle);
        }

        public Edge this[Dimension dimension, bool positiveDirection]
        {
            get => Edges[ToIndex(dimension, positiveDirection)];
            set => Edges[ToIndex(dimension, positiveDirection)] = value;
        }

        private int ToIndex(Dimension dimension, bool positiveDirection)
        {
            return ((int)dimension) * 2 + (positiveDirection ? 1 : 0);
        }
    }
}
