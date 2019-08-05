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

        public Mesh ReadPoints(string offFilePath)
        {
            string[] lines = FileReader.ReadFile(offFilePath);

            if (lines[0] == "OFF")
            {
                return ReadFile(lines, hasNormals: false);
            }
            else if (lines[0] == "NOFF")
            {
                return ReadFile(lines, hasNormals: true);
            }
            else
            {
                throw new ArgumentException($"{offFilePath} is not a valid off file");
            }
        }

        private Mesh ReadFile(string[] lines, bool hasNormals)
        {
            string[] itemCounts = SplitLine(lines[1]);
            int pointCount = int.Parse(itemCounts[0]);
            int faceCount = int.Parse(itemCounts[1]);

            Vector3[] positions = new Vector3[pointCount];
            Vector3[] normals = hasNormals ? new Vector3[pointCount] : null;

            for (int i = 0; i < pointCount; i++)
            {
                string[] lineData = SplitLine(lines[i + 2]);
                positions[i] = GetPosition(lineData);

                if (hasNormals)
                {
                    normals[i] = GetNormal(lineData);
                }
            }

            IFace[] faces = new IFace[faceCount];
            for (int i = 0; i < faceCount; i++)
            {
                string[] lineData = SplitLine(lines[i + pointCount + 2]);
                faces[i] = ProcessFaceLine(lineData);
            }

            return new Mesh(positions, faces, normals);
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

        private Vector3 GetPosition(string[] lineData)
        {
            Vector3 position = new Vector3(
                ParseFloat(lineData[0]),
                ParseFloat(lineData[1]),
                ParseFloat(lineData[2]));

            return position;
        }

        private Vector3 GetNormal(string[] lineData)
        {
            Vector3 normal = new Vector3(
                ParseFloat(lineData[3]),
                ParseFloat(lineData[4]),
                ParseFloat(lineData[5]));

            return normal;
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
