using GeometricAlgorithms.Domain;
using GeometricAlgorithms.OpenTk.Rendering;
using GeometricAlgorithms.OpenTk.Shaders;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.OpenTk
{
    public class Model<TVertex> where TVertex : struct, IVertex
    {
        public TVertex[] Vertices;
        public uint[] Indices;

        internal RenderObject<TVertex> Buffers;

        public Model(TVertex[] vertices, uint[] indices, Shader shader,
                PrimitiveType primitiveType,
                PolygonMode polygonMode,
                MaterialFace materialFace)
        {
            Vertices = vertices ?? throw new ArgumentNullException(nameof(vertices));
            Indices = indices ?? throw new ArgumentNullException(nameof(indices));

            Buffers = new RenderObject<TVertex>(Vertices, Indices, shader, primitiveType, polygonMode, materialFace);
        }

        public void Draw()
        {
            Buffers.Render();
        }
    }
}
