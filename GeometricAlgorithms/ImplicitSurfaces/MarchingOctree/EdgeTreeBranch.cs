using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometricAlgorithms.MeshQuerying;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree
{
    class EdgeTreeBranch : EdgeTreeNode
    {
        public readonly EdgeTreeNode[,,] Children;

        public EdgeTreeBranch(EdgeTreeNode parent, Point parentOffset, OctreeBranch branch)
            : base(parent, parentOffset, branch)
        {
            Children = new EdgeTreeNode[2, 2, 2];
        }

        public override Edge QueryChildrenEdge(EdgeOrientation edgeOrientation)
        {
            throw new NotImplementedException();
        }

        public override FunctionValue QueryChildrenFunctionValue(FunctionValueOrientation functionValueOrientation)
        {
            throw new NotImplementedException();
        }

        public override Side QueryChildrenSide(SideOrientation sideOrientation)
        {
            throw new NotImplementedException();
        }

        public override Edge QueryParentsEdge(EdgeOrientation edgeOrientation, Point childOffset)
        {
            throw new NotImplementedException();
        }

        public override FunctionValue QueryParentsFunctionValue(FunctionValueOrientation functionValueOrientation, Point childOffset)
        {
            throw new NotImplementedException();
        }

        public override Side QueryParentsSide(SideOrientation sideOrientation, Point childOffset)
        {
            throw new NotImplementedException();
        }
    }
}
