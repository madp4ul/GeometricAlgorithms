using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometricAlgorithms.Domain;
using GeometricAlgorithms.MeshQuerying;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree
{
    class EdgeTreeBranch : EdgeTreeNode, IEnumerable<EdgeTreeNode>
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
            int index = functionValueOrientation.GetArrayIndex();

            if (FunctionValues[index] != null)
            {
                return FunctionValues[index];
            }

            Dimension[] insideAxis = functionValueOrientation.GetInsideDimensions(childOffset);

            FunctionValue result;

            if (insideAxis.Length == 3)
            {

                result = QueryInsideFunctionValueForChild(functionValueOrientation, childOffset);
            }
            else if (insideAxis.Length == 2)
            {
                result = QueryOutsideAreaFunctionValueForChild(functionValueOrientation, childOffset, insideAxis);
            }
            else if (insideAxis.Length == 1)
            {
                result = QueryFunctionValueOnEdgeForChild(functionValueOrientation, childOffset, insideAxis);
            }
            else
            {
                result = Parent?.QueryFunctionValueForChild(functionValueOrientation, ParentOffset);
            }

            if (result != null)
            {
                FunctionValues[index] = result;
            }

            return result;
        }

        private FunctionValue QueryFunctionValueOnEdgeForChild(
            FunctionValueOrientation functionValueOrientation,
            OctreeOffset childOffset,
            Dimension[] insideAxis)
        {
            FunctionValueOrientation mirrored = functionValueOrientation.GetMirrored(insideAxis[0]);
            var mirroredChildOffset = childOffset.ToggleDimension(insideAxis[0]);
            FunctionValue fromMirroredChild = this[mirroredChildOffset]?.QueryFunctionValueForParent(mirrored);

            if (fromMirroredChild != null)
            {
                return fromMirroredChild;
            }

            Dimension[] outsideDimension = functionValueOrientation.GetOutsideDimensions(childOffset);

            EdgeOrientation edgeOrientation = new EdgeOrientation(
               dim1: outsideDimension[0],
               dim1Positive: functionValueOrientation.IsMaximum(outsideDimension[0]),
               dim2: outsideDimension[1],
               dim2Positive: functionValueOrientation.IsMaximum(outsideDimension[1]));

            Edge parentEdge = Parent?.QueryEdgeForChild(edgeOrientation, ParentOffset);

            if (parentEdge != null)
            {
                return parentEdge.GetChild(0)?.Maximum
                    ?? parentEdge.GetChild(1).Minimum;
            }

            return null;
        }

        private FunctionValue QueryInsideFunctionValueForChild(
            FunctionValueOrientation functionValueOrientation,
            OctreeOffset childOffset)
        {
            //functionvalue is in the middle of children
            return this.Where(otherChild => otherChild != null && !otherChild.ParentOffset.Equals(childOffset))
              .Select(otherChild =>
              {
                  var valueOrientationInOtherChild = functionValueOrientation;

                  foreach (Dimension dimension in childOffset.GetDifferingDemensions(otherChild.ParentOffset))
                  {
                      valueOrientationInOtherChild = valueOrientationInOtherChild.GetMirrored(dimension);
                  }

                  return otherChild.QueryFunctionValueForParent(valueOrientationInOtherChild);
              })
              .FirstOrDefault();
        }

        private FunctionValue QueryOutsideAreaFunctionValueForChild(
            FunctionValueOrientation functionValueOrientation,
            OctreeOffset childOffset,
            Dimension[] insideAxis)
        {
            //function value is in middle of outside area
            Dimension outsideAxis = functionValueOrientation.GetOutsideDimensions(childOffset)[0];
            bool outsideAtMaximum = functionValueOrientation.IsMaximum(outsideAxis);

            SideOrientation sideOrientation = new SideOrientation(outsideAxis, outsideAtMaximum);
            Side parentSide = Parent?.QuerySideForChild(sideOrientation, ParentOffset);

            if (parentSide == null)
            {
                return null;
            }

            for (int a = 0; a < 2; a++)
            {
                for (int b = 0; b < 2; b++)
                {
                    Side childSide = parentSide.GetChildSide(a, b);

                    if (childSide == null)
                    {
                        continue;
                    }

                    FunctionValue value = getValue(a, b) ?? getValue(b, a);

                    if (value != null)
                    {
                        return value;
                    }

                    FunctionValue getValue(int indexOfOrientedDimension, int indexOfUnOrientedDimension)
                    {
                        EdgeOrientation orientationOnChild = new EdgeOrientation(
                            dim1: outsideAxis,
                            dim1Positive: outsideAtMaximum,
                            dim2: insideAxis[indexOfOrientedDimension],
                            dim2Positive: indexOfOrientedDimension == 0);

                        Edge childEdge = childSide.GetEdge(orientationOnChild);

                        if (childEdge == null)
                        {
                            return null;
                        }

                        return indexOfUnOrientedDimension == 0
                            ? childEdge.Maximum
                            : childEdge.Minimum;
                    }
                }
            }

            return null;
        }

        public override FunctionValue QueryFunctionValueForParent(FunctionValueOrientation functionValueOrientation)
        {
            var offset = new OctreeOffset(
                functionValueOrientation.IsXMaximum ? 1 : 0,
                functionValueOrientation.IsYMaximum ? 1 : 0,
                functionValueOrientation.IsZMaximum ? 1 : 0);

            return this[offset]?.QueryFunctionValueForParent(functionValueOrientation);
        }

        public Edge QueryEdgeForChild(EdgeOrientation edgeOrientation, OctreeOffset childOffset)
        {
            int index = edgeOrientation.GetArrayIndex();

            if (Edges[index] != null)
            {
                return Edges[index];
            }

            Dimension[] edgeAxis = edgeOrientation.GetAxis();

            int insideAxis = edgeAxis
                .Select(axis => (edgeOrientation.IsPositive(axis) ? 0 : 1) == childOffset.GetValue(axis))
                .Where(axisInside => axisInside)
                .Count();

            Edge result;

            if (insideAxis == 2)
            {
                result = QueryInsideEdgeForChild(edgeOrientation, childOffset, edgeAxis);
            }
            else if (insideAxis == 1)
            {
                result = QueryOutsideAreaEdgeForChild(edgeOrientation, childOffset, edgeAxis);
            }
            else
            {
                result = QueryOutsideEdgeForChild(edgeOrientation, childOffset, edgeAxis);
            }

            if (result?.IsComplete ?? false)
            {
                Edges[index] = result;
            }

            return result;
        }

        private Edge QueryOutsideEdgeForChild(EdgeOrientation edgeOrientation, OctreeOffset childOffset, Dimension[] edgeAxis)
        {
            //no child shares the edge. Only query parent for its edge and get its child edge

            var parentEdge = Parent?.QueryEdgeForChild(edgeOrientation, ParentOffset);

            if (parentEdge == null)
            {
                return null;
            }

            int childEdgeIndex = childOffset.ExcludeDimensions(edgeAxis);

            return parentEdge.GetChild(childEdgeIndex);
        }

        private Edge QueryOutsideAreaEdgeForChild(EdgeOrientation edgeOrientation, OctreeOffset childOffset, Dimension[] edgeAxis)
        {
            //query the 1 child that shares the edge then query for the side that contains the edge and get the edge from that

            Dimension dimensionToMirror = edgeAxis.Single(axis => (edgeOrientation.IsPositive(axis) ? 0 : 1) == childOffset.GetValue(axis));

            OctreeOffset mirroredChildOffset = childOffset.ToggleDimension(dimensionToMirror);

            var mirroredEdgeOrientation = edgeOrientation.GetMirrored(dimensionToMirror);
            Edge edgeOfMirroredChild = this[mirroredChildOffset]?.QueryEdgeForParent(mirroredEdgeOrientation);

            if (edgeOfMirroredChild != null && edgeOfMirroredChild.IsComplete)
            {
                return edgeOfMirroredChild;
            }
            else
            {
                Dimension sideAxis = edgeAxis.Single(d => d != dimensionToMirror);
                var sideOrientation = new SideOrientation(sideAxis, edgeOrientation.IsPositive(sideAxis));

                Side side = Parent?.QuerySideForChild(sideOrientation, ParentOffset);

                if (side == null)
                {
                    return null;
                }

                Edge edgeFromDirectChild = side.GetChildSide(childOffset.ExcludeDimension(sideAxis)).GetEdge(edgeOrientation);
                Edge edgeFromMirroredChild = side.GetChildSide(mirroredChildOffset.ExcludeDimension(sideAxis)).GetEdge(mirroredEdgeOrientation);

                var edges = new[] { edgeOfMirroredChild, edgeFromDirectChild, edgeFromMirroredChild }
                     .Where(edge => edge != null);

                return Edge.Merge(edges);
            }
        }

        private Edge QueryInsideEdgeForChild(EdgeOrientation edgeOrientation, OctreeOffset childOffset, Dimension[] edgeAxis)
        {
            //only query 3 children that share the edge and merge the edges if incomplete to get the best result.
            var neighbourEdges = this.Where(child =>
                    child != null
                    && !childOffset.Equals(child.ParentOffset)
                    && childOffset.HasOnlyDifferencesOnDimensions(edgeAxis, child.ParentOffset))
                 .Select(edgeSharingChild =>
                 {
                     bool dim0Different = edgeSharingChild.ParentOffset.GetValue(edgeAxis[0]) != childOffset.GetValue(edgeAxis[0]);
                     bool dim1Different = edgeSharingChild.ParentOffset.GetValue(edgeAxis[1]) != childOffset.GetValue(edgeAxis[1]);

                     EdgeOrientation mirrored = edgeOrientation;
                     if (dim0Different)
                     {
                         mirrored = mirrored.GetMirrored(edgeAxis[0]);
                     }
                     if (dim1Different)
                     {
                         mirrored = mirrored.GetMirrored(edgeAxis[1]);
                     }

                     return edgeSharingChild.QueryEdgeForParent(mirrored);
                 })
                 .ToList();

            return Edge.Merge(neighbourEdges);
        }

        public override Edge QueryEdgeForParent(EdgeOrientation edgeOrientation)
        {
            int index = edgeOrientation.GetArrayIndex();

            if (Edges[index] != null)
            {
                return Edges[index];
            }
            else
            {
                var edgeAxis = edgeOrientation.GetAxis();

                int[] dimensionValues = new int[]
                {
                    edgeOrientation.IsPositive(edgeAxis[0]) ? 1 : 0,
                    edgeOrientation.IsPositive(edgeAxis[1]) ? 1 : 0
                };

                OctreeOffset minOffset = OctreeOffset.WithValuesAtDimension(
                   dimension1: edgeAxis[0],
                   valueAtDimension1: dimensionValues[0],
                   dimension2: edgeAxis[1],
                   valueAtDimension2: dimensionValues[1],
                   otherValue: 0);

                OctreeOffset maxOffset = OctreeOffset.WithValuesAtDimension(
                   dimension1: edgeAxis[0],
                   valueAtDimension1: dimensionValues[0],
                   dimension2: edgeAxis[1],
                   valueAtDimension2: dimensionValues[1],
                   otherValue: 1);

                Edge minChild = this[minOffset]?.QueryEdgeForParent(edgeOrientation);
                Edge maxChild = this[maxOffset]?.QueryEdgeForParent(edgeOrientation);

                var edge = new Edge(minChild, maxChild);

                if (edge.IsComplete)
                {
                    Edges[index] = edge;
                }

                return edge;
            }
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
                var parentSide = Parent?.QuerySideForChild(sideOrientation, ParentOffset);

                if (parentSide == null)
                {
                    return null;
                }

                SideOffset childSidePosition = childOffset.ExcludeDimension(sideOrientation.GetDirection());
                return parentSide.GetChildSide(childSidePosition);
            }
            else
            {
                Dimension sideDirection = sideOrientation.GetDirection();
                OctreeOffset oppositeChildOffset = childOffset.ToggleDimension(sideDirection);

                SideOrientation oppositeOrientation = sideOrientation.GetMirrored();

                return this[oppositeChildOffset]?.QuerySideForParent(oppositeOrientation);
            }
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
                        childSides[i, j] = childWithSide?.QuerySideForParent(sideOrientation);
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

        public IEnumerator<EdgeTreeNode> GetEnumerator()
        {
            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < 2; y++)
                {
                    for (int z = 0; z < 2; z++)
                    {
                        yield return Children[x, y, z];
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
