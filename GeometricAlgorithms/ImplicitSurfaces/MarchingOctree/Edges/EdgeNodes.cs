using GeometricAlgorithms.Extensions;
using GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.Edges
{
    class EdgeNodes
    {
        private readonly RefinementTreeNode[] Nodes = new RefinementTreeNode[4];
        private readonly EdgeNodes ParentEdgeNodes;

        public EdgeNodes(EdgeNodes parentEdgeNodes)
        {
            ParentEdgeNodes = parentEdgeNodes;
        }

        public RefinementTreeNode this[EdgeOrientation orientation]
        {
            get => GetNode(ToIndex(orientation));
            set => Nodes[ToIndex(orientation)] = value;
        }

        /// <summary>
        /// Get node at index. if no node available, ask parent at index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private RefinementTreeNode GetNode(int index)
        {
            var currentNodes = this;
            var result = Nodes[index];

            while (result == null && currentNodes.ParentEdgeNodes != null)
            {
                currentNodes = currentNodes.ParentEdgeNodes;
                result = currentNodes.Nodes[index];
            }

            return result;
        }

        private int ToIndex(EdgeOrientation orientation)
        {
            var axis = orientation.GetAxis();
            int index0 = orientation.IsPositive(axis[0]) ? 1 : 0;
            int index1 = orientation.IsPositive(axis[1]) ? 1 : 0;

            return 2 * index1 + index0;
        }

        public IEnumerable<RefinementTreeNode> GetLessRefinedNeighboursForNode(RefinementTreeNode node)
        {
            //node should be part of nodes array. Dont look at its index
            //because it is not its own neighbour.
            int indexOfNode = Nodes.FirstIndexOrDefault(n => n == node) ?? throw new ArgumentException();

            //Less refined nodes can be found in parent
            if (ParentEdgeNodes != null)
            {
                for (int i = 0; i < Nodes.Length; i++)
                {
                    if (i != indexOfNode)
                    {
                        var foundNeighbour = ParentEdgeNodes.GetNode(i);

                        if (foundNeighbour != null && !foundNeighbour.HasChildren)
                        {
                            yield return foundNeighbour;
                        }
                    }
                }
            }
        }
    }
}
