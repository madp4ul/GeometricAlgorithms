using GeometricAlgorithms.Domain;
using GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.Nodes;
using GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.PointPartitioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.RefinementTrees
{
    /// <summary>
    /// Create this if node priorities are dependant on triangulation. This will automatically
    /// recalculate priorities when creating an triangulation.
    /// </summary>
    /// <typeparam name="TCompareNode"></typeparam>
    class TriangulationBasedPriorityTree<TCompareNode> : RefinementTree<TCompareNode> where TCompareNode : IComparable<TCompareNode>
    {
        internal TriangulationBasedPriorityTree(
            IImplicitSurface implicitSurface,
            OctreePartitioning partitioning,
            Func<RefinementTreeNode, TCompareNode> getComparableFeature)
            : base(implicitSurface, partitioning, getComparableFeature)
        {
        }

        public override Mesh CreateApproximation()
        {
            var approximation = base.CreateApproximation();
            RecalculatePriorities();
            return approximation;
        }

        protected override void RefineNextNode()
        {
            var current = TreeLeafsByRefinementPriority.Dequeue();

            var retriangulatedNeighbours = current.Node.CreateChildren();

            //ValidateTriangulation(Root);

            var removed = TreeLeafsByRefinementPriority.RemoveWhere(i => retriangulatedNeighbours.Contains(i.Node));

            foreach (var compareItem in removed)
            {
                compareItem.RecalculatePriority();
                TreeLeafsByRefinementPriority.Enqueue(compareItem);
            }

            EnqueueChildren(current);
        }

        private void ValidateTriangulation(RefinementTreeNode node)
        {
            if (node.Triangulation != null)
            {
                foreach (var triangle in node.Triangulation)
                {
                    if (!triangle.IsValid)
                    {

                    }
                }
            }

            if (node.HasChildren)
            {
                for (int x = 0; x < 2; x++)
                {
                    for (int y = 0; y < 2; y++)
                    {
                        for (int z = 0; z < 2; z++)
                        {
                            ValidateTriangulation(node.Children[x, y, z]);
                        }
                    }
                }
            }
        }

        public void RecalculatePriorities()
        {
            var recalculatedQueue = new PriorityQueue<ComparableRefinementTreeNode<TCompareNode>>();

            foreach (var item in TreeLeafsByRefinementPriority)
            {
                item.RecalculatePriority();
                recalculatedQueue.Enqueue(item);
            }

            TreeLeafsByRefinementPriority = recalculatedQueue;
        }
    }
}
