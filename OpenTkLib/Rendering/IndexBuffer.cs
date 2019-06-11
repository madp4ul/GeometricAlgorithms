using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.OpenTk.Rendering
{
    class IndexBuffer : IDisposable
    {
        private int OpenGlPointer;

        public readonly int Length;

        public IndexBuffer(uint[] indices, BufferUsageHint bufferUsage = BufferUsageHint.StaticDraw)
        {
            Length = indices.Length;

            OpenGlPointer = GL.GenBuffer();

            GL.BindBuffer(
                BufferTarget.ElementArrayBuffer,
                OpenGlPointer);

            GL.BufferData(
                BufferTarget.ElementArrayBuffer,
                indices.Length * sizeof(uint),
                indices,
                bufferUsage);
        }

        public void Use()
        {
            GL.BindBuffer(
                BufferTarget.ElementArrayBuffer,
                OpenGlPointer);
        }

        public void Dispose()
        {
            //Make sure the currently bound buffer is not this one
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            //Delete buffer
            GL.DeleteBuffer(OpenGlPointer);
        }
    }
}
