using GeometricAlgorithms.Domain.VertexTypes;
using GeometricAlgorithms.OpenTk.Shaders;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.OpenTk.Rendering
{
    static class VertexDeclarations
    {
        public static IVertexDeclaration CreatePosition(Shader shader)
        {
            return new VertexDeclaration<Vertex>(shader)
               .AddAttribute(nameof(Vertex.Position), 3, VertexAttribPointerType.Float, 3 * sizeof(float));
        }

        public static IVertexDeclaration CreatePositionNormal(Shader shader)
        {
            return new VertexDeclaration<VertexNormal>(shader)
               .AddAttribute(nameof(VertexNormal.Position), 3, VertexAttribPointerType.Float, 3 * sizeof(float))
               .AddAttribute(nameof(VertexNormal.Normal), 3, VertexAttribPointerType.Float, 3 * sizeof(float));
        }

        public static IVertexDeclaration GetDeclaration<TVertex>(Shader shader)
        {
            if (typeof(TVertex) == typeof(Vertex))
            {
                return CreatePosition(shader);
            }
            else if (typeof(TVertex) == typeof(VertexNormal))
            {
                return CreatePositionNormal(shader);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
