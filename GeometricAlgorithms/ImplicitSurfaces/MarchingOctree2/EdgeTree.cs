using GeometricAlgorithms.ImplicitSurfaces.MarchingOctree2.PointPartitioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree2
{
    class EdgeTree
    {
        private readonly EdgeTreeNode Root;

        private readonly ImplicitSurfaceProvider ImplicitSurfaceProvider;
        private readonly Func<EdgeTreeNode, int> GetComparationFeature;
        private readonly PriorityQueue<ComparableEdgeTreeNode> TreeLeafsByRefinementPriority;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="implicitSurface"></param>
        /// <param name="partitioning"></param>
        /// <param name="getComparableFeature">Function to extract a value from edge-tree-nodes by which they will be compared.
        /// Nodes with lowest extracted value will be refined first</param>
        public EdgeTree(IImplicitSurface implicitSurface, OctreePartitioning partitioning, Func<EdgeTreeNode, int> getComparableFeature)
        {
            ImplicitSurfaceProvider = new ImplicitSurfaceProvider(implicitSurface);

            Root = EdgeTreeNode.CreateRoot(partitioning.Root, ImplicitSurfaceProvider);

            GetComparationFeature = getComparableFeature ?? throw new ArgumentNullException(nameof(getComparableFeature));

            TreeLeafsByRefinementPriority = new PriorityQueue<ComparableEdgeTreeNode>();
            TreeLeafsByRefinementPriority.Enqueue(new ComparableEdgeTreeNode(Root, getComparableFeature));
        }

        public SurfaceApproximation CreateApproximation()
        {
            var treeLeafs = TreeLeafsByRefinementPriority.Select(cn => cn.Node).ToList();
            treeLeafs.Sort(CompareNodeDepth);

            var approximation = new SurfaceApproximation();

            foreach (var treeLeaf in treeLeafs)
            {
                //todo create triagulation and add to approximation
            }

            throw new NotImplementedException();

            return approximation;
        }

        private static int CompareNodeDepth(EdgeTreeNode node1, EdgeTreeNode node2)
        {
            //invert sorting. Biggest first
            return node2.Depth - node1.Depth;
        }

        public void RefineEdgeTree(int sampleLimit)
        {
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

                            TreeLeafsByRefinementPriority.Enqueue(new ComparableEdgeTreeNode(child, GetComparationFeature));
                        }
                    }
                }
            }
        }

        public static EdgeTree CreateWithWithMostPointsFirst(IImplicitSurface implicitSurface, OctreePartitioning partitioning)
        {
            int getFeature(EdgeTreeNode node) => -node.OctreeNode.VerticesCount;

            return new EdgeTree(implicitSurface, partitioning, getFeature);
        }

        private class ComparableEdgeTreeNode : IComparable<ComparableEdgeTreeNode>
        {
            public readonly EdgeTreeNode Node;
            public readonly int ComparationFeature;

            public ComparableEdgeTreeNode(EdgeTreeNode node, Func<EdgeTreeNode, int> getComparationFeature)
            {
                Node = node ?? throw new ArgumentNullException(nameof(node));
                ComparationFeature = getComparationFeature(node);
            }

            public int CompareTo(ComparableEdgeTreeNode other)
            {
                return ComparationFeature - other.ComparationFeature;
            }
        }
    }
}
