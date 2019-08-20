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
        public EdgeTreeLeaf(EdgeTreeBranch parent, OctreeOffset parentOffset, TreeLeaf leaf, SurfaceResult result)
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

        private FunctionValue FindFunctionValueInTree(FunctionValueOrientation functionValueOrientation)
            => Parent.QueryFunctionValueForChild(functionValueOrientation, ParentOffset);

        private Edge FindEdgeInTree(EdgeOrientation edgeOrientation)
            => Parent.QueryEdgeForChild(edgeOrientation, ParentOffset);

        private Side FindSideInTree(SideOrientation sideOrientation)
            => Parent.QuerySideForChild(sideOrientation, ParentOffset);

        public override Edge QueryEdgeForParent(EdgeOrientation edgeOrientation)
        {
            int index = edgeOrientation.GetArrayIndex();

            return Edges[index];
        }

        public override FunctionValue QueryFunctionValueForParent(FunctionValueOrientation functionValueOrientation)
        {
            int index = functionValueOrientation.GetArrayIndex();

            return FunctionValues[index];
        }

        public override Side QuerySideForParent(SideOrientation sideOrientation)
        {
            int index = sideOrientation.GetArrayIndex();

            return Sides[index];
        }
    }
}
