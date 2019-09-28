using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Tasks;
using GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.PointPartitioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree
{
    public class EdgeTree<TCompareNode> : IEdgeTree where TCompareNode : IComparable<TCompareNode>
    {
        private readonly EdgeTreeNode Root;

        public readonly ImplicitSurfaceProvider ImplicitSurfaceProvider;
        private readonly Func<EdgeTreeNode, TCompareNode> GetComparationFeature;
        private readonly PriorityQueue<ComparableEdgeTreeNode<TCompareNode>> TreeLeafsByRefinementPriority;

        ImplicitSurfaceProvider IEdgeTree.ImplicitSurfaceProvider => ImplicitSurfaceProvider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="implicitSurface"></param>
        /// <param name="partitioning"></param>
        /// <param name="getComparableFeature">Function to extract a value from edge-tree-nodes by which they will be compared.
        /// Nodes with lowest extracted value will be refined first</param>
        internal EdgeTree(IImplicitSurface implicitSurface, OctreePartitioning partitioning, Func<EdgeTreeNode, TCompareNode> getComparableFeature)
        {
            ImplicitSurfaceProvider = new ImplicitSurfaceProvider(implicitSurface);

            Root = EdgeTreeNode.CreateRoot(partitioning.Root, ImplicitSurfaceProvider);

            GetComparationFeature = getComparableFeature ?? throw new ArgumentNullException(nameof(getComparableFeature));

            TreeLeafsByRefinementPriority = new PriorityQueue<ComparableEdgeTreeNode<TCompareNode>>();
            TreeLeafsByRefinementPriority.Enqueue(new ComparableEdgeTreeNode<TCompareNode>(Root, getComparableFeature));
        }

        public Mesh CreateApproximation()
        {
            var treeLeafs = TreeLeafsByRefinementPriority.Select(cn => cn.Node).ToList();
            treeLeafs.Sort(CompareNodeDepth);

            var approximation = new SurfaceApproximation();

            for (int i = 0; i < treeLeafs.Count; i++)
            {
                treeLeafs[i].AddTriangulation(approximation);
            }

            //foreach (var treeLeaf in treeLeafs)
            //{
            //    treeLeaf.AddTriangulation(approximation);
            //}

            return new Mesh(approximation.GetPositions(), approximation.GetFaces());
        }

        private static int CompareNodeDepth(EdgeTreeNode node1, EdgeTreeNode node2)
        {
            //invert sorting. Biggest first
            return node2.Depth - node1.Depth;
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
                var current = TreeLeafsByRefinementPriority.Dequeue();

                current.Node.CreateChildren();

                for (int x = 0; x < 2; x++)
                {
                    for (int y = 0; y < 2; y++)
                    {
                        for (int z = 0; z < 2; z++)
                        {
                            var child = current.Node.Children[x, y, z];

                            TreeLeafsByRefinementPriority.Enqueue(new ComparableEdgeTreeNode<TCompareNode>(child, GetComparationFeature));
                        }
                    }
                }

                int operationsFromInteration = ImplicitSurfaceProvider.FunctionValueCount - completedOperations;

                operationUpdater.UpdateAddOperation(operationsFromInteration);
                completedOperations += operationsFromInteration;
            }

            operationUpdater.IsCompleted();
        }

        private class ComparableEdgeTreeNode<TCompare> : IComparable<ComparableEdgeTreeNode<TCompare>> where TCompare : IComparable<TCompare>
        {
            public readonly EdgeTreeNode Node;
            public readonly TCompare ComparationFeature;

            public ComparableEdgeTreeNode(EdgeTreeNode node, Func<EdgeTreeNode, TCompare> getComparationFeature)
            {
                Node = node ?? throw new ArgumentNullException(nameof(node));
                ComparationFeature = getComparationFeature(node);
            }

            public int CompareTo(ComparableEdgeTreeNode<TCompare> other)
            {
                return ComparationFeature.CompareTo(other.ComparationFeature);
            }
        }
    }
}
