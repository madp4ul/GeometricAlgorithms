using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Vertices;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.FileProcessing
{
    public class OFFReader : IReader
    {
        private readonly FileReader FileReader;

        public OFFReader()
        {
            FileReader = new FileReader();
        }

        public GenericVertex[] ReadPoints(string offFilePath)
        {
            string[] lines = FileReader.ReadFile(offFilePath);

            if (lines[0] == "OFF")
            {
                string[] itemCounts = lines[1].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                int pointCount = int.Parse(itemCounts[0]);

                GenericVertex[] points = new GenericVertex[pointCount];
                for (int i = 0; i < pointCount; i++)
                {
                    string[] vertexDimensions = lines[i + 2].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    Vector3 vertex = new Vector3(
                        float.Parse(vertexDimensions[0], CultureInfo.InvariantCulture),
                        float.Parse(vertexDimensions[1], CultureInfo.InvariantCulture),
                        float.Parse(vertexDimensions[2], CultureInfo.InvariantCulture));

                    points[i] = new GenericVertex(vertex);
                }

                return points;
            }
            else
            {
                throw new ArgumentException($"{offFilePath} is not a valid off file");
            }
        }
    }
}
