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
            if (!IsFirstSameAsLast)
            {
                throw new InvalidOperationException();
            }

            //The start and end elements are duplicate at the start.
            //Remove one and make the segment a real circle by connecting it.
            TriangleLineSegmentNode.Connect(Last, First.Next);
            MergedNodes.Add(Last);

            var triangles = new List<Triangle>();

            var reductions = MergedNodes.Select(n => new PolynomReduction(n)).ToList();

            //TODO handle non-convex flat circles by considering angles (hard because we only have indices and not the spacial data)

            int reductionIndex = 0;
            bool canReduce = true;

            //Rotate reductions until last triangle was added
            while (canReduce)
            {
                PolynomReduction current = reductions[reductionIndex];

                //A reduction can become invalid if multiple reduction exist for the same node
                //and another reduction processed the node already.
                //This will happen if the number of nodes gets reduced but the number of reductions stays the same.
                if (!current.IsValid)
                {
                    reductions.RemoveAt(reductionIndex);
                }
                else
                {
                    canReduce = current.MoveToNextPoint(out Triangle triangle);
                    triangles.Add(triangle);

                    reductionIndex++;
                }

                reductionIndex %= reductions.Count;
            }

            return triangles;
        }

        protected override TriangleLineSegment CreateWithReversedNodes(TriangleLineSegmentNode first, TriangleLineSegmentNode last)
        {
            return new MergedTriangleLineSegment(first, last, MergedNodes);
        }

        protected override string GetName() => "merged line segment";

        private enum PointToSelect
        {
            Next,
            Previous
        }
    }
}
