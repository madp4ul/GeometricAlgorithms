using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingCubes
{
    /// <summary>
    /// documentation at
    /// http://paulbourke.net/geometry/polygonise/
    /// </summary>
    class Cube
    {
        public readonly FunctionValueGrid FunctionValues;
        public readonly EdgeValueGrid Edges;

        public readonly Point CubeCoordinate;

        public readonly FunctionValue[] CornerValues;

        public Cube(FunctionValueGrid source, EdgeValueGrid edges, Point coord)
        {
            FunctionValues = source ?? throw new ArgumentNullException(nameof(source));
            Edges = edges;
            CubeCoordinate = coord;

            //following illustration in documentation
            CornerValues = new FunctionValue[8]
            {
                source.GetValue(coord.X,coord.Y,coord.Z),
                source.GetValue(coord.X+1,coord.Y,coord.Z),
                source.GetValue(coord.X+1,coord.Y,coord.Z+1),
                source.GetValue(coord.X,coord.Y,coord.Z+1),
                source.GetValue(coord.X,coord.Y+1,coord.Z),
                source.GetValue(coord.X+1,coord.Y+1,coord.Z),
                source.GetValue(coord.X+1,coord.Y+1,coord.Z+1),
                source.GetValue(coord.X,coord.Y+1,coord.Z+1),
            };
        }

        public List<Triangle> ComputeTriangles()
        {
            var result = new List<Triangle>();
            int cubeIndex = 0;
            int[] vertexIndices = new int[12];

            // Determine the index into the edge table which
            // tells us which vertices are inside of the surface
            if (CornerValues[0].Value < 0) cubeIndex |= 1;
            if (CornerValues[1].Value < 0) cubeIndex |= 2;
            if (CornerValues[2].Value < 0) cubeIndex |= 4;
            if (CornerValues[3].Value < 0) cubeIndex |= 8;
            if (CornerValues[4].Value < 0) cubeIndex |= 16;
            if (CornerValues[5].Value < 0) cubeIndex |= 32;
            if (CornerValues[6].Value < 0) cubeIndex |= 64;
            if (CornerValues[7].Value < 0) cubeIndex |= 128;

            /* Cube is entirely in/out of the surface */
            if (Tables.EdgeTable[cubeIndex] == 0)
                return result;

            void selectIndex(int index, int cornerIndex1, int cornerIndex2)
            {
                var container = Edges.GetEdgeIndex(CubeCoordinate, (EdgeIndex)index);
                vertexIndices[index] = container.HasIndex
                    ? container.Index
                    : Edges.AddVertexToEdge(container, VertexInterp(CornerValues[cornerIndex1], CornerValues[cornerIndex2]));
            }

            /* Find the vertices where the surface intersects the cube */
            if ((Tables.EdgeTable[cubeIndex] & 1) > 0)
                selectIndex(0, 0, 1);
            if ((Tables.EdgeTable[cubeIndex] & 2) > 0)
                selectIndex(1, 1, 2);
            if ((Tables.EdgeTable[cubeIndex] & 4) > 0)
                selectIndex(2, 2, 3);
            if ((Tables.EdgeTable[cubeIndex] & 8) > 0)
                selectIndex(3, 3, 0);
            if ((Tables.EdgeTable[cubeIndex] & 16) > 0)
                selectIndex(4, 4, 5);
            if ((Tables.EdgeTable[cubeIndex] & 32) > 0)
                selectIndex(5, 5, 6);
            if ((Tables.EdgeTable[cubeIndex] & 64) > 0)
                selectIndex(6, 6, 7);
            if ((Tables.EdgeTable[cubeIndex] & 128) > 0)
                selectIndex(7, 7, 4);
            if ((Tables.EdgeTable[cubeIndex] & 256) > 0)
                selectIndex(8, 0, 4);
            if ((Tables.EdgeTable[cubeIndex] & 512) > 0)
                selectIndex(9, 1, 5);
            if ((Tables.EdgeTable[cubeIndex] & 1024) > 0)
                selectIndex(10, 2, 6);
            if ((Tables.EdgeTable[cubeIndex] & 2048) > 0)
                selectIndex(11, 3, 7);

            /* Create the triangle */
            for (int i = 0; Tables.TriangleIndexTable[cubeIndex, i] != -1; i += 3)
            {
                Triangle triangle = new Triangle(
                    vertexIndices[Tables.TriangleIndexTable[cubeIndex, i]],
                    vertexIndices[Tables.TriangleIndexTable[cubeIndex, i + 1]],
                    vertexIndices[Tables.TriangleIndexTable[cubeIndex, i + 2]]
                );

                result.Add(triangle);
            }

            return result;
        }

        // Linearly interpolate the position where an isosurface cuts
        // an edge between two vertices, each with their own scalar value

        Vector3 VertexInterp(FunctionValue f1, FunctionValue f2)
        {
            const float interpolationMinimum = 0.00001f;

            float absValue1 = Math.Abs(f1.Value);
            if (absValue1 < interpolationMinimum)
                return f1.Position;

            float absValue2 = Math.Abs(f2.Value);
            if (absValue2 < interpolationMinimum)
                return f2.Position;

            if (Math.Abs(f1.Value - f2.Value) < interpolationMinimum)
                return f1.Position;
            float interpolation = absValue1 / (absValue1 + absValue2);

            return f1.Position + interpolation * (f2.Position - f1.Position);
        }
    }

    public class Triangle2
    {
        public Vector3 C1;
        public Vector3 C2;
        public Vector3 C3;

        public Triangle2(Vector3 c1, Vector3 c2, Vector3 c3)
        {
            C1 = c1;
            C2 = c2;
            C3 = c3;
        }
    }
}
