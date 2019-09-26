using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree2
{
    class SideInsideEdges
    {
        private readonly Edge[,] Edges;

        public SideInsideEdges(ImplicitSurfaceProvider implicitSurface, SideOutsideEdges outsideEdges)
        {
            Edges = new Edge[2, 2];

            FunctionValue minValue = outsideEdges[0, 0].MinValue;
            FunctionValue maxValue = outsideEdges[1, 1].MaxValue;

            Vector3 middlePosition = Vector3.Interpolate(minValue.Position, maxValue.Position, 0.5f);
            FunctionValue sideMiddleValue = implicitSurface.CreateFunctionValue(middlePosition);

            foreach (var edge in outsideEdges.Where(e => !e.HasChildren))
            {
                edge.CreateChildren();
            }

            FunctionValue minDimensionMinDirectionMiddleValue = outsideEdges[0, 0].Children[0].MaxValue;
            FunctionValue minDimensionMaxDirectionMiddleValue = outsideEdges[0, 1].Children[0].MaxValue;
            FunctionValue maxDimensionMinDirectionMiddleValue = outsideEdges[1, 0].Children[0].MaxValue;
            FunctionValue maxDimensionMaxDirectionMiddleValue = outsideEdges[1, 1].Children[0].MaxValue;

            Dimension[] minDimensionsForEdges = new Dimension[2]
            {
                outsideEdges.Dimensions.DirectionAxisFromCubeCenter,
                outsideEdges.Dimensions.SideAxis[0]
            };

            this[0, 0] = new Edge(implicitSurface, minDimensionsForEdges, minDimensionMinDirectionMiddleValue, sideMiddleValue);
            this[0, 1] = new Edge(implicitSurface, minDimensionsForEdges, sideMiddleValue, minDimensionMaxDirectionMiddleValue);

            Dimension[] maxDimensionsForEdges = new Dimension[2]
            {
                outsideEdges.Dimensions.DirectionAxisFromCubeCenter,
                outsideEdges.Dimensions.SideAxis[1]
            };

            this[1, 0] = new Edge(implicitSurface, maxDimensionsForEdges, maxDimensionMinDirectionMiddleValue, sideMiddleValue);
            this[1, 1] = new Edge(implicitSurface, maxDimensionsForEdges, sideMiddleValue, maxDimensionMaxDirectionMiddleValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dimensionIndex">0 is min dimension, 1 is max dimension</param>
        /// <param name="directionIndex">0 is negative direction, 1 is positive direction</param>
        /// <returns></returns>
        public Edge this[int dimensionIndex, int directionIndex]
        {
            get { return Edges[dimensionIndex, directionIndex]; }
            private set { Edges[dimensionIndex, directionIndex] = value; }
        }
    }
}
