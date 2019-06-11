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
    class VertexBuffer<TData> : IDisposable where TData : struct
    {
        private int OpenGlPointer;

        public VertexBuffer(TData[] dataArray, int elementSize, BufferUsageHint bufferUsage = BufferUsageHint.StaticDraw)
        {
            OpenGlPointer = GL.GenBuffer();

            GL.BindBuffer(
                BufferTarget.ArrayBuffer,
                OpenGlPointer);

            GL.BufferData(
                BufferTarget.ArrayBuffer,
                dataArray.Length * elementSize,
                dataArray,
                bufferUsage);
        }

        public static VertexBuffer<Vector3> GetVectorBuffer(Vector3[] vectors)
        {
            return new VertexBuffer<Vector3>(vectors, Vector3.SizeInBytes);
        }
        public static VertexBuffer<float> GetFloatBuffer(float[] floats)
        {
            return new VertexBuffer<float>(floats, sizeof(float));
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
