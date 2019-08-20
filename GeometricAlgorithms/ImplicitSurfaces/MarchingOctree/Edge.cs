using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree
{
    class Edge
    {
        public FunctionValue Start;
        public FunctionValue End;

        /// <summary>
        /// if the edge is not completed no valid function values can be retrieved from it.
        /// however the child edges still may be complete and can be used
        /// </summary>
        public bool IsComplete => Start != null && End != null;

        /// <summary>
        /// How far away the vertex is from the start. only has a value
        /// if function values cross zero (surface) on this edge
        /// </summary>
        public float? InterpolationValue;

        /// <summary>
        /// Index of Vertex on this edge. only has a value
        /// if function values cross zero (surface) on this edge
        /// </summary>
        public int? VertexIndex;

        private Edge[] Children;

        public Edge(FunctionValue start, FunctionValue end)
        {
            Start = start ?? throw new ArgumentNullException(nameof(start));
            End = end ?? throw new ArgumentNullException(nameof(end));

            Children = new Edge[2];

            //TODO compute interpolation and add vertex to result
        }

        public Edge(Edge child0, Edge child1)
        {
            Children = new Edge[2]
            {
                child0,
                child1
            };

            Start = child0.Start;
            End = child1.End;
        }
    }
}
