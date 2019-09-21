using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree2.Triangulation
{
    class MergedTriangleLineSegment : TriangleLineSegment
    {
        public readonly List<TriangleLineSegmentNode> MergedNodes;

        public MergedTriangleLineSegment(TriangleLineSegmentNode first, TriangleLineSegmentNode last)
            : this(first, last, new List<TriangleLineSegmentNode>())
        { }

        public MergedTriangleLineSegment(TriangleLineSegmentNode first, TriangleLineSegmentNode last, List<TriangleLineSegmentNode> mergedNodes)
            : base(first, last)
        {
            MergedNodes = mergedNodes ?? throw new ArgumentNullException(nameof(mergedNodes));
        }

        public List<Triangle> TriangulateCircle()
        {
            if (!IsCircle)
            {
                throw new InvalidOperationException();
            }

            //TODO

            throw new NotImplementedException();
        }
    }
}
