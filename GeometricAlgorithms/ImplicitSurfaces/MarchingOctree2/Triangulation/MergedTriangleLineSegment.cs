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

            //The start and end elements are duplicate at the start.
            //Remove one and make the segment a real circle by connecting it.
            TriangleLineSegmentNode.Connect(Last, First.Next);
            MergedNodes.Add(Last);

            var triangles = new List<Triangle>();

            var reductions = MergedNodes.Select(PolynomReduction.CreateInitial).ToList();

            //TODO handle non-convex flat circles by considering angles (hard because we only have indices and not the spacial data)

            int reductionIndex = 0;
            bool canReduce = true;

            //Rotate reductions until last triangle was added
            while (canReduce)
            {
                PolynomReduction current = reductions[reductionIndex];
                canReduce = current.MoveToNextPoint(out Triangle triangle);
                triangles.Add(triangle);

                reductionIndex = (reductionIndex + 1) % reductions.Count;
            }

            return triangles;
        }

        protected override string Name => "merged line segment";

        private enum PointToSelect
        {
            Next,
            Previous
        }
    }
}
