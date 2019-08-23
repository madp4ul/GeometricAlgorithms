using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometricAlgorithms.Domain;
using GeometricAlgorithms.MeshQuerying;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree
{
    class EdgeTreeLeaf : EdgeTreeNode
    {
        public EdgeTreeLeaf(EdgeTreeBranch parent, OctreeOffset parentOffset, TreeLeaf leaf, IImplicitSurface surface, SurfaceResult result)
            : base(parent, parentOffset, leaf)
        {
            //TODO get functionvalues or compute them

            //1. query for sides and store them in cube if found
            //for each found side take its edges in side-space and convert their orientation to cube-space
            //and store them in the cube if edge not already there from a previously found side.
            //for each stored edge, store its functionvalues in cube if not already there from other edge
            QueryForSides();

            //2. for each edge that wasnt found yet, query for it
            //and store it and its function values if not already there
            QueryForEdges();

            //3. for each function value that wasnt found yet, query for it and store it
            QueryForFunctionValues();

            //4. for each function value that wasnt found yet, compute it and store it
            ComputeMissingFunctionValues(surface);

            //5. for each edge that wasnt found yet, compute it from function values and store it
            ComputeMissingEdges(result);

            //6. for each side that wasnt found yet, compute it from edges and store it
            ComputeMissingSides();

            //7. Compute triangles at the end and put them into result
        }

        private void QueryForSides()
        {
            //query for sides and store them in cube if found
            //for each found side take its edges in side-space and convert their orientation to cube-space
            //and store them in the cube if edge not already there from a previously found side.
            //for each stored edge, store its functionvalues in cube if not already there from other edge

            for (int i = 0; i < Sides.Length; i++)
            {
                var sideOrientation = new SideOrientation(SideOrientation.GetSideIndex(i));
                Side side = Parent.QuerySideForChild(sideOrientation, ParentOffset);

                if (side != null)
                {
                    Sides[i] = side;

                    foreach (var orientedEdge in side.GetEdges(sideOrientation))
                    {
                        AddEdge(orientedEdge);
                    }
                }
            }
        }

        private void QueryForEdges()
        {
            //for each edge that wasnt found yet, query for it
            //and store it and its function values if not already there
            for (int i = 0; i < Edges.Length; i++)
            {
                if (Edges[i] == null)
                {
                    var edgeOrientation = new EdgeOrientation(EdgeOrientation.GetEdgeIndex(i));
                    Edge edge = Parent.QueryEdgeForChild(edgeOrientation, ParentOffset);

                    if (edge != null)
                    {
                        AddEdge(new OrientedEdge(edge, edgeOrientation));
                    }
                }
            }
        }

        private void QueryForFunctionValues()
        {
            for (int i = 0; i < FunctionValues.Length; i++)
            {
                if (FunctionValues[i] == null)
                {
                    var orientation = new FunctionValueOrientation(FunctionValueOrientation.GetFunctionValueIndex(i));
                    FunctionValue value = Parent.QueryFunctionValueForChild(orientation, ParentOffset);

                    if (value != null)
                    {
                        AddFunctionValue(new OrientedFunctionValue(value, orientation));
                    }
                }
            }
        }

        private void ComputeMissingFunctionValues(IImplicitSurface surface)
        {
            for (int i = 0; i < FunctionValues.Length; i++)
            {
                if (FunctionValues[i] == null)
                {
                    var orientation = new FunctionValueOrientation(FunctionValueOrientation.GetFunctionValueIndex(i));

                    Vector3 position = orientation.GetCorner(OctreeNode.BoundingBox);
                    float value = surface.GetApproximateSurfaceDistance(position);

                    var functionValue = new FunctionValue(position, value);

                    FunctionValues[i] = functionValue;
                }
            }
        }

        private void ComputeMissingEdges(SurfaceResult result)
        {
            for (int i = 0; i < Edges.Length; i++)
            {
                if (Edges[i] == null)
                {
                    var orientation = new EdgeOrientation(EdgeOrientation.GetEdgeIndex(i));

                    var minValueOrientation = orientation.GetValueOrientation(0);
                    var maxValueOrientation = orientation.GetValueOrientation(1);

                    FunctionValue minValue = FunctionValues[minValueOrientation.GetArrayIndex()];
                    FunctionValue maxValue = FunctionValues[maxValueOrientation.GetArrayIndex()];

                    Edge edge = new Edge(minValue, maxValue, result);

                    Edges[i] = edge;
                }
            }
        }

        private void ComputeMissingSides()
        {
            for (int i = 0; i < Sides.Length; i++)
            {
                if (Sides[i] == null)
                {
                    var orientation = new SideOrientation(SideOrientation.GetSideIndex(i));

                    Edge getEdge(int axisIndex, int minmax) => Edges[orientation.GetEdgeOrientation(axisIndex, minmax).GetArrayIndex()];

                    Side side = new Side(orientation.GetDirection(),
                         smallerDimMin: getEdge(0, 0),
                         smallerDimMax: getEdge(0, 1),
                         biggerDimMin: getEdge(1, 0),
                         biggerDimMax: getEdge(1, 1));

                    Sides[i] = side;
                }
            }
        }

        private void AddEdge(OrientedEdge orientedEdge)
        {
            int edgeIndex = orientedEdge.Orientation.GetArrayIndex();

            if (orientedEdge.HasEdge && Edges[edgeIndex] == null)
            {
                Edges[edgeIndex] = orientedEdge.Edge;

                foreach (var functionValue in orientedEdge.Edge.GetFunctionValues(orientedEdge.Orientation))
                {
                    AddFunctionValue(functionValue);
                }
            }
        }

        private void AddFunctionValue(OrientedFunctionValue orientedFunctionValue)
        {
            int valueIndex = orientedFunctionValue.Orientation.GetArrayIndex();

            if (orientedFunctionValue.HasFunctionValue && FunctionValues[valueIndex] == null)
            {
                FunctionValues[valueIndex] = orientedFunctionValue.FunctionValue;
            }
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
