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


        public Mesh<VertexNormal> ReadPoints(string filePath)
        {
            Mesh<VertexNormal> model;

            if (filePath.EndsWith("off"))
            {
                model = new OFFReader().ReadPoints(filePath);
            }
            else
            {
                throw new NotImplementedException("This file format is not supported");
            }

            ScaleAndMoveToUnitCubeAroundOrigin(model);

            return model;
        }

        private void ScaleAndMoveToUnitCubeAroundOrigin(Mesh<VertexNormal> model)
        {
            var bounds = BoundingBox.CreateContainer(model.Vertices);

            Vector3 translationBeforeScaling = bounds.Minimum;//Move Corner to Origin
            Vector3 size = bounds.Diagonal;
            float scaleFactor = 1 / Math.Max(size.X, Math.Max(size.Y, size.Z));

            Vector3 scaledSize = size * scaleFactor;

            Vector3 translationAfterScaling =
                (Vector3.One - scaledSize) / 2 //Move to Middle of Unit Cube
                - (Vector3.One / 2); //Move from middle of Unit Cube to Origin

            for (int i = 0; i < model.Vertices.Length; i++)
            {
                ref VertexNormal vertex = ref model.Vertices[i];
                vertex.Position -= translationBeforeScaling;
                vertex.Position *= scaleFactor;
                vertex.Position += translationAfterScaling;
            }
        }
    }
}
