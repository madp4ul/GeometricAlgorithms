using GeometricAlgorithms.Domain;
using GeometricAlgorithms.MeshQuerying;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree
{
    class EdgeTreeNode
    {
        //4 edges

        public int Level;

        public readonly EdgeTreeNode Parent;
        public readonly Point ParentOffset;
        public readonly EdgeTreeNode[,,] Children;

        public readonly ATreeNode OctreeNode;
        public BoundingBox Boundaries => OctreeNode.BoundingBox;

        private Edge[] Edges;

        public EdgeTreeNode(EdgeTreeNode parent, Point parentOffset, ATreeNode octreeNode)
        {
            Parent = parent;
            ParentOffset = parentOffset;
            OctreeNode = octreeNode;

            Edges = new Edge[12];
            Children = new EdgeTreeNode[2, 2, 2];
        }

        public void Compute()
        {

        }
    }
}
