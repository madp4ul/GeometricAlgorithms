using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.OpenTk.Shaders
{
    class Shader : IDisposable
    {
        protected readonly int Handle;

        public Shader(string vertexPath, string fragmentPath)
        {
            //Read sources
            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, ReadSource(vertexPath));

            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, ReadSource(fragmentPath));

            //compile sources
            CompileShader(vertexShader);
            CompileShader(fragmentShader);

            //create shader
            Handle = GL.CreateProgram();
            GL.AttachShader(Handle, vertexShader);
            GL.AttachShader(Handle, fragmentShader);
            GL.LinkProgram(Handle);

            //remove individual shaders
            GL.DetachShader(Handle, vertexShader);
            GL.DetachShader(Handle, fragmentShader);
            GL.DeleteShader(fragmentShader);
            GL.DeleteShader(vertexShader);
        }

        internal void Use()
        {
            GL.UseProgram(Handle);
        }

        private void CompileShader(int shaderPointer)
        {
            GL.CompileShader(shaderPointer);
            string infoLog = GL.GetShaderInfoLog(shaderPointer);
            if (infoLog != string.Empty)
                throw new InvalidOperationException(infoLog);
        }

        private string ReadSource(string path)
        {
            string source;

            using (StreamReader reader = new StreamReader(path, Encoding.UTF8))
            {
                source = reader.ReadToEnd();
            }

            return source;
        }

        private bool isDisposed = false;
        public void Dispose()
        {
            if (!isDisposed)
            {
                GL.DeleteProgram(Handle);

                isDisposed = true;
            }
            GC.SuppressFinalize(this);
        }
        ~Shader()
        {
            GL.DeleteProgram(Handle);
        }
    }
}
