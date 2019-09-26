using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree
{
    class EdgeChildren
    {
        private readonly Edge[] Edges = new Edge[2];

        public EdgeChildren(Edge minEdge, Edge maxEdge)
        {
            Edges[0] = minEdge ?? throw new ArgumentNullException(nameof(minEdge));
            Edges[1] = maxEdge ?? throw new ArgumentNullException(nameof(maxEdge));
        }

        /// <summary>
        /// 0 for min edge, 1 for max edge
        /// </summary>
        /// <param name="isMinEdge"></param>
        /// <returns></returns>
        public Edge this[int isMinEdge]
        {
            get => Edges[isMinEdge];
            private set => Edges[isMinEdge] = value;
        }
    }
}
