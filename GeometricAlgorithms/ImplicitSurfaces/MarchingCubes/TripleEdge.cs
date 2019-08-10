using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingCubes
{
    class TripleEdge
    {
        public IndexContainer IndexOfX;
        public IndexContainer IndexOfY;
        public IndexContainer IndexOfZ;

        public TripleEdge()
        {
            IndexOfX = new IndexContainer();
            IndexOfY = new IndexContainer();
            IndexOfZ = new IndexContainer();
        }
    }
}
