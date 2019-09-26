using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree2.PointPartitioning
{
    class OctreePartitioning
    {
        public readonly OctreeNode Root;

        public OctreePartitioning(Mesh mesh, float containerScale = 1.3f)
        {
            var positionMapping = mesh.Positions
                .Select((vector, index) => new PositionIndex(vector, index))
                .ToArray();

            var range = Range<PositionIndex>.FromArray(positionMapping, 0, mesh.VertexCount);
            var boundingBox = BoundingBox.CreateContainer(mesh.Positions);
            boundingBox.GrowToCube();
            boundingBox.ScaleAroundCenter(containerScale);

            Root = new OctreeNode(range, boundingBox);
        }
    }
}
