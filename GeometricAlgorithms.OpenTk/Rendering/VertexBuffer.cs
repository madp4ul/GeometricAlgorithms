using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.VertexTypes;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.OpenTk.Rendering
{
    class VertexBuffer<TVertex> : IVertexBuffer, IDisposable where TVertex : struct, IVertex
    {
        private int OpenGlPointer;

        public VertexBuffer(TVertex[] vertices, BufferUsageHint bufferUsage = BufferUsageHint.StaticDraw)
        {
            int vertexSize = System.Runtime.InteropServices.Marshal.SizeOf(typeof(TVertex));

            OpenGlPointer = GL.GenBuffer();

            GL.BindBuffer(
                BufferTarget.ArrayBuffer,
                OpenGlPointer);

            GL.BufferData(
                BufferTarget.ArrayBuffer,
                vertices.Length * vertexSize,
                vertices,
                bufferUsage);
        }

        public void Dispose()
        {
            //Make sure the currently bound buffer is not this one
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            //Delete buffer
            GL.DeleteBuffer(OpenGlPointer);
        }

        public void Use()
        {
            GL.BindBuffer(
                BufferTarget.ArrayBuffer,
                OpenGlPointer);
        }
    }
}
