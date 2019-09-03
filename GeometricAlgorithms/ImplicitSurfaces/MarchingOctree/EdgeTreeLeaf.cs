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
            ComputeMissingFunctionValues(surface, result);

            //5. for each edge that wasnt found yet, compute it from function values and store it
            ComputeMissingEdges(result);

            //6. for each side that wasnt found yet, compute it from edges and store it
            ComputeMissingSides(result);

            //7. Compute triangles at the end and put them into result
            ComputeTriangles(result);
        }

        private void ComputeTriangles(SurfaceResult result)
        {
            int cubeIndex = 0;

            if (FunctionValues[0].Value < 0) cubeIndex |= 1;
            if (FunctionValues[1].Value < 0) cubeIndex |= 2;
            if (FunctionValues[2].Value < 0) cubeIndex |= 4;
            if (FunctionValues[3].Value < 0) cubeIndex |= 8;
            if (FunctionValues[4].Value < 0) cubeIndex |= 16;
            if (FunctionValues[5].Value < 0) cubeIndex |= 32;
            if (FunctionValues[6].Value < 0) cubeIndex |= 64;
            if (FunctionValues[7].Value < 0) cubeIndex |= 128;

            /* Cube is entirely in/out of the surface */
            if (Tables.EdgeTable[cubeIndex] == 0)
                return;

            /* Create the triangle */
            for (int i = 0; Tables.TriangleIndexTable[cubeIndex, i] != -1; i += 3)
            {
                int edgeIndex0 = Tables.TriangleIndexTable[cubeIndex, i];
                int edgeIndex1 = Tables.TriangleIndexTable[cubeIndex, i + 1];
                int edgeIndex2 = Tables.TriangleIndexTable[cubeIndex, i + 2];

                AddTriangleToSides(edgeIndex0, edgeIndex1, edgeIndex2);

                Triangle triangle = new Triangle(
                    Edges[edgeIndex0].GetVertexIndex(),
                    Edges[edgeIndex1].GetVertexIndex(),
                    Edges[edgeIndex2].GetVertexIndex()
                );

                result.AddFace(triangle);
            }
        }

        private void AddTriangleToSides(int edgeIndex0, int edgeIndex1, int edgeIndex2)
        {
            var orientation1 = new EdgeOrientation(EdgeOrientation.GetEdgeIndex(edgeIndex0));
            var orientation2 = new EdgeOrientation(EdgeOrientation.GetEdgeIndex(edgeIndex1));
            var orientation3 = new EdgeOrientation(EdgeOrientation.GetEdgeIndex(edgeIndex2));

            void addTriangleEdge(EdgeOrientation edgeStart, EdgeOrientation edgeEnd)
            {
                if (SideOrientation.TryGetContainingOrientation(edgeStart, edgeEnd, out SideOrientation sideOrientation))
                {
                    Sides[sideOrientation.GetArrayIndex()].AddTriangleEdge(
                        new TriangleEdge(
                            Edges[edgeStart.GetArrayIndex()],
                            Edges[edgeEnd.GetArrayIndex()],
                            sideOrientation.GetDirection()));
                }
            }

            addTriangleEdge(orientation1, orientation2);
            addTriangleEdge(orientation2, orientation3);
            addTriangleEdge(orientation3, orientation1);
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
                Side side = Parent?.QuerySideForChild(sideOrientation, ParentOffset);

                if (side != null)
                {
                    if (side.IsComplete)
                    {
                        Sides[i] = side;
                    }

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
                    Edge edge = Parent?.QueryEdgeForChild(edgeOrientation, ParentOffset);

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
                    FunctionValue value = Parent?.QueryFunctionValueForChild(orientation, ParentOffset);

                    if (value != null)
                    {
                        AddFunctionValue(new OrientedFunctionValue(value, orientation));
                    }
                }
            }
        }

        private void ComputeMissingFunctionValues(IImplicitSurface surface, SurfaceResult result)
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

                    result.AddFunctionValue(functionValue);
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

        private void ComputeMissingSides(SurfaceResult result)
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
                         biggerDimMax: getEdge(1, 1),
                         result: result);

                    Sides[i] = side;
                }
            }
        }

        private void AddEdge(OrientedEdge orientedEdge)
        {
            int edgeIndex = orientedEdge.Orientation.GetArrayIndex();

            if (orientedEdge.HasEdge && Edges[edgeIndex] == null)
            {
                if (orientedEdge.Edge.IsComplete)
                {
                    Edges[edgeIndex] = orientedEdge.Edge;
                }

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
                var valuePosition = new Vector3(
                     orientedFunctionValue.Orientation.IsXMaximum ? OctreeNode.BoundingBox.Maximum.X : OctreeNode.BoundingBox.Minimum.X,
                     orientedFunctionValue.Orientation.IsYMaximum ? OctreeNode.BoundingBox.Maximum.Y : OctreeNode.BoundingBox.Minimum.Y,
                     orientedFunctionValue.Orientation.IsZMaximum ? OctreeNode.BoundingBox.Maximum.Z : OctreeNode.BoundingBox.Minimum.Z);

                if (orientedFunctionValue.FunctionValue.Position != valuePosition)
                {
                    throw new ApplicationException();
                }

                FunctionValues[valueIndex] = orientedFunctionValue.FunctionValue;
            }
        }

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

        public override string ToString()
        {
            return $"{{edgetree leaf: parent offset {ParentOffset}}}";
        }
    }
}
