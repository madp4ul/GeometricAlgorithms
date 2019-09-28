using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree
{
    public interface IRefinementTree
    {
        ImplicitSurfaceProvider ImplicitSurfaceProvider { get; }

        void RefineEdgeTree(int sampleLimit, IProgressUpdater progressUpdater);
        Mesh CreateApproximation();
    }
}
