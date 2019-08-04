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
                return ReadFile(lines, ProcessOFFVertexLine);
            }
            else if (lines[0] == "NOFF")
            {
                return ReadFile(lines, ProcessNOFFVertexLine);
            }
            else
            {
                throw new ArgumentException($"{offFilePath} is not a valid off file");
            }
        }

        private GenericVertex[] ReadFile(string[] lines, Func<string[], GenericVertex> processLine)
        {
            string[] itemCounts = SplitLine(lines[1]);
            int pointCount = int.Parse(itemCounts[0]);
            int faceCount = int.Parse(itemCounts[1]);

            GenericVertex[] points = new GenericVertex[pointCount];
            for (int i = 0; i < pointCount; i++)
            {
                string[] lineData = SplitLine(lines[i + 2]);
                points[i] = processLine(lineData);
            }

            return points;
        }

        private GenericVertex ProcessOFFVertexLine(string[] lineData)
        {
            Vector3 vertex = new Vector3(
                ParseFloat(lineData[0]),
                ParseFloat(lineData[1]),
                ParseFloat(lineData[2]));

            return new GenericVertex(vertex);
        }

        private GenericVertex ProcessNOFFVertexLine(string[] lineData)
        {
            GenericVertex vertex = ProcessOFFVertexLine(lineData);

            Vector3 normal = new Vector3(
                ParseFloat(lineData[3]),
                ParseFloat(lineData[4]),
                ParseFloat(lineData[5]));

            vertex.Data = normal;

            return vertex;
        }

        private float ParseFloat(string floatString)
        {
            return float.Parse(floatString, CultureInfo.InvariantCulture);
        }

        private string[] SplitLine(string line)
        {
            return line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
