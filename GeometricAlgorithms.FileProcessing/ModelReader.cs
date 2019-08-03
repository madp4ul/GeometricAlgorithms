using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Vertices;

namespace GeometricAlgorithms.FileProcessing
{
    public class ModelReader : IReader
    {


        public GenericVertex[] ReadPoints(string filePath)
        {
            GenericVertex[] vertices;

            if (filePath.EndsWith("off"))
            {
                vertices = new OFFReader().ReadPoints(filePath);
            }
            else
            {
                throw new NotImplementedException("This file format is not supported");
            }

            ScaleAndMoveTo1(vertices);

            return vertices;
        }

        private void ScaleAndMoveTo1(GenericVertex[] vertices)
        {
            var bounds = BoundingBox.CreateContainer(vertices);

            Vector3 translation = bounds.Minimum;
            Vector3 size = (bounds.Maximum - bounds.Minimum);
            float scaleFactor = 1 / Math.Max(size.X, Math.Max(size.Y, size.Z));

            for (int i = 0; i < vertices.Length; i++)
            {
                ref GenericVertex vertex = ref vertices[i];
                vertex.Position -= translation;
                vertex.Position *= scaleFactor;
            }
        }
    }
}
