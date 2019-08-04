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

            ScaleAndMoveToUnitCube(model);

            return model;
        }

        private void ScaleAndMoveToUnitCube(Mesh<VertexNormal> model)
        {
            var bounds = BoundingBox.CreateContainer(model.Vertices);

            Vector3 translation = bounds.Minimum;
            Vector3 size = (bounds.Maximum - bounds.Minimum);
            float scaleFactor = 1 / Math.Max(size.X, Math.Max(size.Y, size.Z));

            for (int i = 0; i < model.Vertices.Length; i++)
            {
                ref VertexNormal vertex = ref model.Vertices[i];
                vertex.Position -= translation;
                vertex.Position *= scaleFactor;
            }
        }
    }
}
