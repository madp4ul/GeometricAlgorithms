using GeometricAlgorithms.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree
{
    class SideOutsideEdges : IEnumerable<Edge>
    {
        public readonly SideDimensions Dimensions;

        private readonly Edge[,] Edges;

        private readonly Func<int>[] DimensionMappings;

        public bool IsEdgeMissing => this.Any(e => e == null);

        public SideOutsideEdges(SideOrientation orientation)
            : this(orientation.GetAxis())
        { }

        public SideOutsideEdges(Dimension directionAxisFromCubeCenter)
        {
            Dimensions = SideDimensions.GetForDirectionAxis(directionAxisFromCubeCenter);

            Edges = new Edge[2, 2];

            //dynamically create functions that map each value of the dimension-enum
            //to an index in the edge-array.
            DimensionMappings = new Func<int>[(int)Dimension.Count];

            bool mappedDirectionAxis = false;
            for (int i = 0; i < DimensionMappings.Length; i++)
            {
                Dimension currentDimension = (Dimension)i;

                if (currentDimension == Dimensions.DirectionAxisFromCubeCenter)
                {
                    mappedDirectionAxis = true;
                    DimensionMappings[i] =
                        () => throw new InvalidOperationException("This side does not have any edges in direction of the given dimension.");
                }
                else
                {
                    int dimensionEdgeIndex = mappedDirectionAxis ? i + 1 : i;
                    DimensionMappings[i] = () => dimensionEdgeIndex;
                }
            }
        }

        public Edge this[Dimension directionAxisFromSideCenter, bool inPositiveDirection]
        {
            get { return Edges[DimensionMappings[(int)directionAxisFromSideCenter](), inPositiveDirection ? 1 : 0]; }
            set { Edges[DimensionMappings[(int)directionAxisFromSideCenter](), inPositiveDirection ? 1 : 0] = value; }
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
            set { Edges[dimensionIndex, directionIndex] = value; }
        }

        public IEnumerator<Edge> GetEnumerator()
        {
            yield return Edges[0, 0];
            yield return Edges[0, 1];
            yield return Edges[1, 0];
            yield return Edges[1, 1];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
