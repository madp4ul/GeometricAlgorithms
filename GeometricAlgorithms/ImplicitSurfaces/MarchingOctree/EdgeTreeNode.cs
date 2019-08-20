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
        public readonly EdgeTreeNode Parent;

        /// <summary>
        /// Index of this node in parents children
        /// </summary>
        public readonly Point ParentOffset;


        public readonly ATreeNode OctreeNode;
        public BoundingBox Boundaries => OctreeNode.BoundingBox;

        protected readonly Edge[] Edges;
        protected readonly FunctionValue[] FunctionValues;
        protected readonly Side[] Sides;

        public EdgeTreeNode(EdgeTreeNode parent, Point parentOffset, ATreeNode octreeNode)
        {
            Parent = parent;
            ParentOffset = parentOffset;
            OctreeNode = octreeNode;

            FunctionValues = new FunctionValue[8];
            Edges = new Edge[12];
            Sides = new Side[6];
        }

        public abstract FunctionValue QueryParentsFunctionValue(FunctionValueOrientation functionValueOrientation, Point childOffset);
        public abstract FunctionValue QueryChildrenFunctionValue(FunctionValueOrientation functionValueOrientation);

        public abstract Edge QueryParentsEdge(EdgeOrientation edgeOrientation, Point childOffset);
        public abstract Edge QueryChildrenEdge(EdgeOrientation edgeOrientation);

        /// <summary>
        /// Child calls this of its parent to get a side if already computed
        /// </summary>
        /// <param name="sideOrientation">the orientation of the side from the child that is calling this</param>
        /// <param name="childOffset">the position of the child inside the parent</param>
        /// <returns>the side or null if nothing found</returns>
        public abstract Side QueryParentsSide(SideOrientation sideOrientation, Point childOffset);

        /// <summary>
        /// Parent calls this of its children to get a side from them
        /// </summary>
        /// <param name="sideOrientation">The orientation of the side relative to the node this is called of</param>
        /// <returns></returns>
        public abstract Side QueryChildrenSide(SideOrientation sideOrientation);
    }
}
