using GeometricAlgorithms.Domain;
using GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.PointPartitioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree
{
    /// <summary>
    /// Create this if node priorities are dependant on triangulation. This will automatically
    /// recalculate priorities when creating an triangulation.
    /// </summary>
    /// <typeparam name="TCompareNode"></typeparam>
    class TriangulationBasedPriorityEdgeTree<TCompareNode> : EdgeTree<TCompareNode> where TCompareNode : IComparable<TCompareNode>
    {
        internal TriangulationBasedPriorityEdgeTree(
            IImplicitSurface implicitSurface,
            OctreePartitioning partitioning,
            Func<EdgeTreeNode, TCompareNode> getComparableFeature)
            : base(implicitSurface, partitioning, getComparableFeature)
        {
        }

        public override Mesh CreateApproximation()
        {
            var approximation = base.CreateApproximation();
            RecalculatePriorities();
            return approximation;
        }

        public void RecalculatePriorities()
        {
            var recalculatedQueue = new PriorityQueue<ComparableEdgeTreeNode<TCompareNode>>();

            foreach (var item in TreeLeafsByRefinementPriority)
            {
                item.RecalculatePriority();
                recalculatedQueue.Enqueue(item);
            }

            TreeLeafsByRefinementPriority = recalculatedQueue;
        }
    }
}
