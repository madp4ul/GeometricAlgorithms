using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometricAlgorithms.MeshQuerying;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree
{
    class EdgeTreeLeaf : EdgeTreeNode
    {
        public EdgeTreeLeaf(EdgeTreeNode parent, Point parentOffset, TreeLeaf leaf, SurfaceResult result)
            : base(parent, parentOffset, leaf)
        {
            //TODO get functionvalues or compute them

            //1. query for sides and store them in cube if found
            //for each found side take its edges in side-space and convert their orientation to cube-space
            //and store them in the cube if edge not already there from a previously found side.
            //for each stored edge, store its functionvalues in cube if not already there from other edge

            //2. for each edge that wasnt found yet, query for it
            //and store it and its function values if not already there

            //3. for each function value that wasnt found yet, query for it and store it

            //4. for each function value that wasnt found yet, compute it and store it

            //5. for each edge that wasnt found yet, compute it from function values and store it

            //6. for each side that wasnt found yet, compute it from edges and store it

            //Note: Put new position values from computed edges into the surface result.
            //      and compute triangles at the end and put them into result
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
