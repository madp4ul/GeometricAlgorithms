using GeometricAlgorithms.Extensions;
using GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.Sides
{
    class SideNodes
    {
        private readonly RefinementTreeNode[] Nodes = new RefinementTreeNode[2];
        private readonly SideNodes ParentSideNodes;

        public SideNodes(SideNodes parentSideNodes)
        {
            ParentSideNodes = parentSideNodes;
        }

        public RefinementTreeNode this[SideOrientation orientation]
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

            while (result == null && currentNodes.ParentSideNodes != null)
            {
                currentNodes = currentNodes.ParentSideNodes;
                result = currentNodes.Nodes[index];
            }

            return result;
        }

        private int ToIndex(SideOrientation orientation)
        {
            return orientation.IsMax ? 1 : 0;
        }

        private int MirrorIndex(int index)
        {
            index ^= 1;

            return index;
        }

        public RefinementTreeNode GetLessRefinedNodeAtMirroredPosition(RefinementTreeNode node)
        {
            //node should be part of nodes array. Dont look at its index
            //because it is not its own neighbour.
            int indexOfNode = Nodes.FirstIndexOrDefault(n => n == node) ?? throw new ArgumentException();

            int mirrored = MirrorIndex(indexOfNode);

            //Less refined nodes can be found in parent
            var mirrorNode = ParentSideNodes?.GetNode(mirrored);

            //Since we start looking in the parents nodes, the found node could have children.
            //But in this case it is not a leaf so return null.
            if (mirrorNode == null || mirrorNode.HasChildren)
            {
                return null;
            }

            return mirrorNode;
        }
    }
}
