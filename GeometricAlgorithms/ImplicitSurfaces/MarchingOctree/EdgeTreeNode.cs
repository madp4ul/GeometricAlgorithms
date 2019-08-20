using GeometricAlgorithms.Domain;
using GeometricAlgorithms.MeshQuerying;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree
{
    abstract class EdgeTreeNode
    {
        public readonly EdgeTreeBranch Parent;

        /// <summary>
        /// Index of this node in parents children
        /// </summary>
        public readonly OctreeOffset ParentOffset;


        public readonly ATreeNode OctreeNode;
        public BoundingBox Boundaries => OctreeNode.BoundingBox;

        protected readonly Edge[] Edges;
        protected readonly FunctionValue[] FunctionValues;
        protected readonly Side[] Sides;

        public EdgeTreeNode(EdgeTreeBranch parent, OctreeOffset parentOffset, ATreeNode octreeNode)
        {
            Parent = parent;
            ParentOffset = parentOffset;
            OctreeNode = octreeNode;

            FunctionValues = new FunctionValue[8];
            Edges = new Edge[12];
            Sides = new Side[6];
        }

        public abstract FunctionValue QueryFunctionValueForParent(FunctionValueOrientation functionValueOrientation);

        public abstract Edge QueryEdgeForParent(EdgeOrientation edgeOrientation);

        /// <summary>
        /// Parent calls this of its children to get a side from them
        /// </summary>
        /// <param name="sideOrientation">The orientation of the side relative to the node this is called of</param>
        /// <returns></returns>
        public abstract Side QuerySideForParent(SideOrientation sideOrientation);
    }
}
