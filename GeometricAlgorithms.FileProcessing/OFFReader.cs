using GeometricAlgorithms.Domain;
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

        public Mesh<VertexNormal> ReadPoints(string offFilePath)
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

        private Mesh<VertexNormal> ReadFile(string[] lines, Func<string[], VertexNormal> processVertexLine)
        {
            string[] itemCounts = SplitLine(lines[1]);
            int pointCount = int.Parse(itemCounts[0]);
            int faceCount = int.Parse(itemCounts[1]);

            VertexNormal[] vertices = new VertexNormal[pointCount];
            for (int i = 0; i < pointCount; i++)
            {
                string[] lineData = SplitLine(lines[i + 2]);
                vertices[i] = processVertexLine(lineData);
            }

            IFace[] faces = new IFace[faceCount];
            for (int i = 0; i < faceCount; i++)
            {
                string[] lineData = SplitLine(lines[i + pointCount + 2]);
                faces[i] = ProcessFaceLine(lineData);
            }

            return new Mesh<VertexNormal>(vertices, new IFace[0]);
        }

        private IFace ProcessFaceLine(string[] lineData)
        {
            if (int.TryParse(lineData[0], out int result) && result == 3)
            {
                return new Triangle(
                    int.Parse(lineData[1]),
                    int.Parse(lineData[2]),
                    int.Parse(lineData[3]));
            }
            else
            {
                throw new NotImplementedException($"Can not parse faces with {lineData[0]} vertices");
            }
        }

        private VertexNormal ProcessOFFVertexLine(string[] lineData)
        {
            Vector3 vertex = new Vector3(
                ParseFloat(lineData[0]),
                ParseFloat(lineData[1]),
                ParseFloat(lineData[2]));

            return new VertexNormal(vertex);
        }

        private VertexNormal ProcessNOFFVertexLine(string[] lineData)
        {
            VertexNormal vertex = ProcessOFFVertexLine(lineData);

            Vector3 normal = new Vector3(
                ParseFloat(lineData[3]),
                ParseFloat(lineData[4]),
                ParseFloat(lineData[5]));

            vertex.OriginalNormal = normal;

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
