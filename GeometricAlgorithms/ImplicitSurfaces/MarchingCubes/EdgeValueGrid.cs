using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Tasks;
using System;
using System.Collections.Generic;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingCubes
{
    public class EdgeValueGrid
    {
        private readonly FunctionValueGrid FunctionValueGrid;

        private readonly TripleEdge[] EdgeValues;

        public readonly List<Vector3> Vertices;

        public Mesh ComputedSurface { get; private set; }

        public Point Steps => FunctionValueGrid.StepAmounts;

        public readonly Point Cubes;
        public readonly int TotalCubes;

        public EdgeValueGrid(FunctionValueGrid functionValueGrid)
        {
            FunctionValueGrid = functionValueGrid;

            Cubes = new Point(Steps.X - 1, Steps.Y - 1, Steps.Z - 1);
            TotalCubes = Cubes.X * Cubes.Y * Cubes.Z;

            Vertices = new List<Vector3>();
            EdgeValues = new TripleEdge[FunctionValueGrid.TotalSteps];

            for (int i = 0; i < EdgeValues.Length; i++)
            {
                EdgeValues[i] = new TripleEdge();
            }

            //TODO
            //1. Compute vertices for all edges
            //2. store existing vertices in 1-dim array and put index into edge array
            //3. make values in edge array findable
        }

        public void Compute(OperationProgressUpdater progressUpdater)
        {
            var triangles = new List<Triangle>();

            //Because cubes already consider the corner at the next index of the current one
            //we dont want cubes for the last row
            Point cubes = Cubes;

            for (int x = 0; x < cubes.X; x++)
            {
                for (int y = 0; y < cubes.Y; y++)
                {
                    for (int z = 0; z < cubes.Z; z++)
                    {
                        Cube cube = new Cube(FunctionValueGrid, this, new Point(x, y, z));
                        triangles.AddRange(cube.ComputeTriangles());
                    }

                    progressUpdater.UpdateAddOperation(operationCount: cubes.Z);
                }
            }

            ComputedSurface = new Mesh(Vertices.ToArray(), triangles.ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coord"></param>
        /// <param name="edgeIndex"></param>
        /// <param name="vertex"></param>
        /// <returns>Index of added vertex in list</returns>
        public int AddVertexToEdge(Point coord, EdgeIndex edgeIndex, Vector3 vertex)
        {
            var edgeIndexContainer = GetEdgeIndex(coord, edgeIndex);

            return AddVertexToEdge(edgeIndexContainer, vertex);
        }

        public int AddVertexToEdge(IndexContainer edgeIndexContainer, Vector3 vertex)
        {
            if (edgeIndexContainer.HasIndex)
            {
                throw new InvalidOperationException();
            }

            Vertices.Add(vertex);

            edgeIndexContainer.Index = Vertices.Count - 1;
            edgeIndexContainer.HasIndex = true;

            return edgeIndexContainer.Index;
        }

        public IndexContainer GetEdgeIndex(Point cubeCoordinate, EdgeIndex edge)
        {
            if (edge == EdgeIndex._000x)
            {
                return GetEdge(cubeCoordinate, Dimension.X);
            }
            if (edge == EdgeIndex._000y)
            {
                return GetEdge(cubeCoordinate, Dimension.Y);
            }
            if (edge == EdgeIndex._000z)
            {
                return GetEdge(cubeCoordinate, Dimension.Z);
            }

            if (edge == EdgeIndex._001x)
            {
                return GetEdge(new Point(cubeCoordinate.X, cubeCoordinate.Y, cubeCoordinate.Z + 1), Dimension.X);
            }
            if (edge == EdgeIndex._001y)
            {
                return GetEdge(new Point(cubeCoordinate.X, cubeCoordinate.Y, cubeCoordinate.Z + 1), Dimension.Y);
            }

            if (edge == EdgeIndex._010x)
            {
                return GetEdge(new Point(cubeCoordinate.X, cubeCoordinate.Y + 1, cubeCoordinate.Z), Dimension.X);
            }
            if (edge == EdgeIndex._010z)
            {
                return GetEdge(new Point(cubeCoordinate.X, cubeCoordinate.Y + 1, cubeCoordinate.Z), Dimension.Z);
            }

            if (edge == EdgeIndex._100y)
            {
                return GetEdge(new Point(cubeCoordinate.X + 1, cubeCoordinate.Y, cubeCoordinate.Z), Dimension.Y);
            }
            if (edge == EdgeIndex._100z)
            {
                return GetEdge(new Point(cubeCoordinate.X + 1, cubeCoordinate.Y, cubeCoordinate.Z), Dimension.Z);
            }

            if (edge == EdgeIndex._101y)
            {
                return GetEdge(new Point(cubeCoordinate.X + 1, cubeCoordinate.Y, cubeCoordinate.Z + 1), Dimension.Y);
            }
            if (edge == EdgeIndex._110z)
            {
                return GetEdge(new Point(cubeCoordinate.X + 1, cubeCoordinate.Y + 1, cubeCoordinate.Z), Dimension.Z);
            }
            if (edge == EdgeIndex._011x)
            {
                return GetEdge(new Point(cubeCoordinate.X, cubeCoordinate.Y + 1, cubeCoordinate.Z + 1), Dimension.X);
            }

            throw new ArgumentException();
        }

        private IndexContainer GetEdge(Point minVertexCoordinate, Dimension dimension)
        {
            TripleEdge tripleEdge = EdgeValues[CoordinateToIndex(minVertexCoordinate)];

            if (dimension == Dimension.X)
            {
                return tripleEdge.IndexOfX;
            }
            else if (dimension == Dimension.Y)
            {
                return tripleEdge.IndexOfY;
            }
            else if (dimension == Dimension.Z)
            {
                return tripleEdge.IndexOfZ;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        private int CoordinateToIndex(Point coord)
        {
            return coord.X * Steps.Y * Steps.Z + coord.Y * Steps.Z + coord.Z;
        }
    }
}