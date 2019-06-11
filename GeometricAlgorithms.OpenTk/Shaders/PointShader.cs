using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.OpenTk.Shaders
{
    class PointShader : Shader
    {
        private const string ShaderFolder = "Shaders/glsl/PointShader";

        public PointShader()
            : base(ShaderFolder + "/shader.vert", ShaderFolder + "/shader.frag")
        {
            //TODO add properties for attributes
            //set VertexAttribPointers
        }
    }
}
