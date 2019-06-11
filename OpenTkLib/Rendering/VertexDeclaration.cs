using GeometricAlgorithms.Domain;
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
    /// <summary>
    /// 
    /// </summary>
    class VertexDeclaration<TVertex> : IVertexDeclaration where TVertex : struct, IVertex
    {
        private readonly int OpenGLPointer;

        private readonly int VertexSize;
        private int OffsetForNextAttribute = 0;

        private Shader Shader;

        public VertexDeclaration(Shader shader)
        {
            Shader = shader ?? throw new ArgumentException("Shader can not be null");

            OpenGLPointer = GL.GenVertexArray();

            VertexSize = System.Runtime.InteropServices.Marshal.SizeOf(typeof(TVertex));
        }

        /// <summary>
        /// link data from TVertex to a shader attribute
        /// </summary>
        /// <param name="name">name of attribute in shader code</param>
        /// <param name="valueCount">amount of values in shader variable</param>
        /// <param name="vertexAttribPointerType">type of values in shader variable</param>
        /// <param name="size">sizeof(type)*valueCount</param>
        /// <returns>self</returns>
        public VertexDeclaration<TVertex> AddAttribute(string name, int valueCount, VertexAttribPointerType vertexAttribPointerType, int size)
        {
            int address = GL.GetAttribLocation(Shader.Handle, name);
            if (address < 0)
            {
                throw new InvalidOperationException($"Shader does not have an attribute with name '{name}'");
            }

            GL.BindVertexArray(OpenGLPointer);
            GL.VertexAttribPointer(address, valueCount, vertexAttribPointerType, false, VertexSize, OffsetForNextAttribute);
            GL.EnableVertexAttribArray(address);


            OffsetForNextAttribute += size;

            return this;
        }

        public void Use()
        {
            GL.BindVertexArray(OpenGLPointer);
        }
    }
}
