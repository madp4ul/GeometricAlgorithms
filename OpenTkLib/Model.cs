using GeometricAlgorithms.Domain.VertexTypes;
using GeometricAlgorithms.OpenTk.Rendering;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.OpenTk
{
    public class Model<TVertex> where TVertex : Vertex
    {
        public TVertex[] Vertices;
        public int[] Indices;
        public PrimitiveType PrimitiveType;
        public PolygonMode PolygonMode;

        internal BufferContainer Buffers;

        public void Draw()
        {

        }
    }
}
