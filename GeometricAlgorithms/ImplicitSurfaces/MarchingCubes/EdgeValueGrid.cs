using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingCubes
{
    public class EdgeValueGrid
    {
        private readonly FunctionValueGrid FunctionValueGrid;

        private readonly TripleEdge[] EdgeValues;

        public readonly List<Vector3> Vertices;

        public int Steps => FunctionValueGrid.Steps;

        public EdgeValueGrid(FunctionValueGrid functionValueGrid)
        {
            FunctionValueGrid = functionValueGrid;

            Vertices = new List<Vector3>();
            EdgeValues = new TripleEdge[Steps * Steps * Steps];

            for (int i = 0; i < EdgeValues.Length; i++)
            {
                EdgeValues[i] = new TripleEdge();
            }

            //TODO
            //1. Compute vertices for all edges
            //2. store existing vertices in 1-dim array and put index into edge array
            //3. make values in edge array findable
        }

        public List<Triangle> Compute()
        {
            var result = new List<Triangle>();

            //Because cubes already consider the corner at the next index of the current one
            //we dont want cubes for the last row
            int cubesLength = Steps - 1;

            for (int x = 0; x < cubesLength; x++)
            {
                for (int y = 0; y < cubesLength; y++)
                {
                    for (int z = 0; z < cubesLength - 1; z++)
                    {
                        Cube cube = new Cube(FunctionValueGrid, this, new Point(x, y, z));
                        result.AddRange(cube.ComputeTriangles());
                    }
                }
            }

            return result;
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

        public IndexContainer GetEdgeIndex(Point coord, EdgeIndex edge)
        {
            if (edge == EdgeIndex._000x)
            {
                return GetEdge(coord, Dimension.X);
            }
            if (edge == EdgeIndex._000y)
            {
                return GetEdge(coord, Dimension.Y);
            }
            if (edge == EdgeIndex._000z)
            {
                return GetEdge(coord, Dimension.Z);
            }

            if (edge == EdgeIndex._001x)
            {
                return GetEdge(new Point(coord.X, coord.Y, coord.Z + 1), Dimension.X);
            }
            if (edge == EdgeIndex._001y)
            {
                return GetEdge(new Point(coord.X, coord.Y, coord.Z + 1), Dimension.Y);
            }

            if (edge == EdgeIndex._010x)
            {
                return GetEdge(new Point(coord.X, coord.Y + 1, coord.Z), Dimension.X);
            }
            if (edge == EdgeIndex._010z)
            {
                return GetEdge(new Point(coord.X, coord.Y + 1, coord.Z), Dimension.Z);
            }

            if (edge == EdgeIndex._100y)
            {
                return GetEdge(new Point(coord.X + 1, coord.Y, coord.Z), Dimension.Y);
            }
            if (edge == EdgeIndex._100z)
            {
                return GetEdge(new Point(coord.X + 1, coord.Y, coord.Z), Dimension.Z);
            }

            if (edge == EdgeIndex._101y)
            {
                return GetEdge(new Point(coord.X + 1, coord.Y, coord.Z + 1), Dimension.Y);
            }
            if (edge == EdgeIndex._110z)
            {
                return GetEdge(new Point(coord.X + 1, coord.Y + 1, coord.Z), Dimension.Z);
            }
            if (edge == EdgeIndex._011x)
            {
                return GetEdge(new Point(coord.X, coord.Y + 1, coord.Z + 1), Dimension.X);
            }

            throw new ArgumentException();
        }

        private IndexContainer GetEdge(Point coord, Dimension dimension)
        {
            TripleEdge tripleEdge = EdgeValues[CoordToIndex(coord)];

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

        private int CoordToIndex(Point coord)
        {
            return coord.X * Steps * Steps + coord.Y * Steps + coord.Z;
        }
    }
}