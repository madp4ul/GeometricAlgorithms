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

        public float? InterpolationVsalue;
        public int? VertexIndex;

        private Edge[] Children;

        public readonly int Depth;

        public Edge(FunctionValue start, FunctionValue end, int depth)
        {
            Start = start ?? throw new ArgumentNullException(nameof(start));
            End = end ?? throw new ArgumentNullException(nameof(end));
            Depth = depth;

            Children = new Edge[2];
        }

        public Edge(Edge child0, Edge child1)
        {
            if (child0.Depth != child1.Depth)
            {
                throw new ArgumentException("edges dont match");
            }

            Children = new Edge[2]
            {
                child0,
                child1
            };
            Depth = child0.Depth + 1;

            Start = child0.Start;
            End = child1.End;
        }
    }
}
