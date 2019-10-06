using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Tasks;
using GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.Approximation;
using GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.Nodes;
using GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.PointPartitioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.RefinementTrees
{
    internal class RefinementTree<TCompareNode> : IRefinementTree where TCompareNode : IComparable<TCompareNode>
    {
        protected readonly RefinementTreeNode Root;
        private readonly RefiningApproximation Approximation;

        public readonly ImplicitSurfaceProvider ImplicitSurfaceProvider;
        protected readonly Func<RefinementTreeNode, TCompareNode> GetComparationFeature;
        protected PriorityQueue<ComparableRefinementTreeNode<TCompareNode>> TreeLeafsByRefinementPriority;

        ImplicitSurfaceProvider IRefinementTree.ImplicitSurfaceProvider => ImplicitSurfaceProvider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="implicitSurface"></param>
        /// <param name="partitioning"></param>
        /// <param name="getComparableFeature">Function to extract a value from edge-tree-nodes by which they will be compared.
        /// Nodes with lowest extracted value will be refined first</param>
        internal RefinementTree(IImplicitSurface implicitSurface, OctreePartitioning partitioning, Func<RefinementTreeNode, TCompareNode> getComparableFeature)
        {
            ImplicitSurfaceProvider = new ImplicitSurfaceProvider(implicitSurface);
            Approximation = new RefiningApproximation();

            Root = RefinementTreeNode.CreateRoot(partitioning.Root, Approximation, ImplicitSurfaceProvider);

            GetComparationFeature = getComparableFeature ?? throw new ArgumentNullException(nameof(getComparableFeature));

            TreeLeafsByRefinementPriority = new PriorityQueue<ComparableRefinementTreeNode<TCompareNode>>();
            TreeLeafsByRefinementPriority.Enqueue(new ComparableRefinementTreeNode<TCompareNode>(Root, getComparableFeature));
        }

        public virtual Mesh CreateApproximation()
        {
            return Approximation.GetApproximation();
        }

        public void RefineEdgeTree(int sampleLimit, IProgressUpdater progressUpdater)
        {
            var operationUpdater = new OperationProgressUpdater(
                progressUpdater,
                totalOperations: sampleLimit - ImplicitSurfaceProvider.FunctionValueCount,
                operationDescription: "Computing implicit surface samples");

            int completedOperations = ImplicitSurfaceProvider.FunctionValueCount;

            while (ImplicitSurfaceProvider.FunctionValueCount < sampleLimit)
            {
                RefineNextNode();

                int operationsFromInteration = ImplicitSurfaceProvider.FunctionValueCount - completedOperations;

                operationUpdater.UpdateAddOperation(operationsFromInteration);
                completedOperations += operationsFromInteration;
            }

            operationUpdater.IsCompleted();
        }

        protected virtual void RefineNextNode()
        {
            var current = TreeLeafsByRefinementPriority.Dequeue();

            current.Node.CreateChildren();

            EnqueueChildren(current);
        }

        protected void EnqueueChildren(ComparableRefinementTreeNode<TCompareNode> current)
        {
            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < 2; y++)
                {
                    for (int z = 0; z < 2; z++)
                    {
                        var child = current.Node.Children[x, y, z];

                        TreeLeafsByRefinementPriority.Enqueue(new ComparableRefinementTreeNode<TCompareNode>(child, GetComparationFeature));
                    }
                }
            }
        }
    }
}
