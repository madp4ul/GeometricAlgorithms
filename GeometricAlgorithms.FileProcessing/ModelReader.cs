using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometricAlgorithms.Domain;

namespace GeometricAlgorithms.FileProcessing
{
    public class ModelReader : IReader
    {
        public Mesh ReadPoints(string filePath)
        {
            Mesh mesh;

            if (filePath.EndsWith("off"))
            {
                mesh = new OFFReader().ReadPoints(filePath);
            }
            else
            {
                throw new NotImplementedException("This file format is not supported");
            }

            return ScaleAndMoveToUnitCubeAroundOrigin(mesh);
        }

        private Mesh ScaleAndMoveToUnitCubeAroundOrigin(Mesh mesh)
        {
            var bounds = BoundingBox.CreateContainer(mesh.Positions);

            Vector3 translationBeforeScaling = bounds.Minimum;//Move Corner to Origin
            Vector3 size = bounds.Diagonal;
            float scaleFactor = 1 / Math.Max(size.X, Math.Max(size.Y, size.Z));

            Vector3 scaledSize = size * scaleFactor;

            Vector3 translationAfterScaling =
                (Vector3.One - scaledSize) / 2 //Move to Middle of Unit Cube
                - (Vector3.One / 2); //Move from middle of Unit Cube to Origin


            Vector3[] tranformedPositions = new Vector3[mesh.VertexCount];

            for (int i = 0; i < mesh.VertexCount; i++)
            {
                Vector3 position = mesh.Positions[i];
                position -= translationBeforeScaling;
                position *= scaleFactor;
                position += translationAfterScaling;
                tranformedPositions[i] = position;
            }

            return mesh.Copy(replacePositions: tranformedPositions);
        }
    }
}
