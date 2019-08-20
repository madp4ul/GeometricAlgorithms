using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometricAlgorithms.Domain;
using GeometricAlgorithms.MeshQuerying;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree
{
    class EdgeTreeBranch : EdgeTreeNode
    {
        public readonly EdgeTreeNode[,,] Children;

        public EdgeTreeBranch(EdgeTreeBranch parent, OctreeOffset parentOffset, OctreeBranch branch)
            : base(parent, parentOffset, branch)
        {
            Children = new EdgeTreeNode[2, 2, 2];
        }

        public EdgeTreeNode this[OctreeOffset childIndex]
        {
            get => Children[childIndex.X, childIndex.Y, childIndex.Z];
        }

        public FunctionValue QueryFunctionValueForChild(FunctionValueOrientation functionValueOrientation, OctreeOffset childOffset)
        {

        }

        public Edge QueryEdgeForChild(EdgeOrientation edgeOrientation, OctreeOffset childOffset)
        {

        }

        /// <summary>
        /// Child calls this of its parent to get a side if already computed
        /// </summary>
        /// <param name="sideOrientation">the orientation of the side from the child that is calling this</param>
        /// <param name="childOffset">the position of the child inside the parent</param>
        /// <returns>the side or null if nothing found</returns>
        public Side QuerySideForChild(SideOrientation sideOrientation, OctreeOffset childOffset)
        {
            bool isOutside =
                (sideOrientation.IsX
                && ((childOffset.X == 0 && !sideOrientation.IsMax) || (childOffset.X == 1 && sideOrientation.IsMax)))
                || (sideOrientation.IsY
                && ((childOffset.Y == 0 && !sideOrientation.IsMax) || (childOffset.Y == 1 && sideOrientation.IsMax)))
                || (sideOrientation.IsZ
                && ((childOffset.Z == 0 && !sideOrientation.IsMax) || (childOffset.Z == 1 && sideOrientation.IsMax)));

            if (isOutside)
            {
                var parentSide = Parent.QuerySideForChild(sideOrientation, ParentOffset);

                if (parentSide != null)
                {
                    SideOffset childSidePosition = childOffset.ExcludeDimension(sideOrientation.GetDirection());

                    return parentSide.GetChildSide(childSidePosition);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                Dimension sideDirection = sideOrientation.GetDirection();
                OctreeOffset oppositeChildOffset = childOffset.ToggleDimension(sideDirection);

                SideOrientation oppositeOrientation = sideOrientation.GetMirrored();

                return this[oppositeChildOffset].QuerySideForParent(oppositeOrientation);
            }
        }

        public override Edge QueryEdgeForParent(EdgeOrientation edgeOrientation)
        {
            //TODO find the right children

            //(if result already stored, return it)

            //return null if there are no children

            //get edge with same orientation from them

            //build edge from the childrens edges

            //(store result)

            //return result

            throw new NotImplementedException();
        }

        public override FunctionValue QueryFunctionValueForParent(FunctionValueOrientation functionValueOrientation)
        {
            throw new NotImplementedException();
        }

        public override Side QuerySideForParent(SideOrientation sideOrientation)
        {
            int index = sideOrientation.GetArrayIndex();

            if (Sides[index] == null)
            {
                Dimension sideDirection = sideOrientation.GetDirection();
                int valueAtDirection = sideOrientation.IsMax ? 1 : 0;

                Side[,] childSides = new Side[2, 2];

                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        OctreeOffset offsetOfChildWithSide = OctreeOffset.WithValueAtDimension(sideDirection, valueAtDirection, i, j);
                        EdgeTreeNode childWithSide = this[offsetOfChildWithSide];

                        //A child may be null. then the child side will also be null and
                        //the result side will not be complete. However it still may contain valid child sides.
                        //We dont return null because the caller of this method could be interested in a child side of 
                        //the result which could be complete.
                        childSides[i, j] = this[offsetOfChildWithSide]?.QuerySideForParent(sideOrientation);
                    }
                }

                var sideFromCombinedChildSides = new Side(
                   dimension: sideOrientation.GetDirection(),
                   child00: childSides[0, 0],
                   child01: childSides[0, 1],
                   child10: childSides[1, 0],
                   child11: childSides[1, 1]);

                //We only cache complete sides because incomplete sides could be completed later.
                //This way we dont have to find a cached version of the side upon completion complete that as well.
                if (sideFromCombinedChildSides.IsComplete)
                {
                    Sides[index] = sideFromCombinedChildSides;
                }

                return sideFromCombinedChildSides;
            }

            return Sides[index];
        }
    }
}
