﻿using GeometricAlgorithms.Domain;
using GeometricAlgorithms.OpenTk.Shaders;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.OpenTk.Rendering
{
    class RenderData<TVertex> where TVertex : struct, IVertex
    {
        public bool IsInitialized { get; set; }

        Shader Shader;

        IVertexBuffer VertexBuffer;
        IndexBuffer IndexBuffer;
        IVertexDeclaration VertexDeclaration;

        public PrimitiveType PrimitiveType;
        public PolygonMode PolygonMode;
        public MaterialFace MaterialFace;

        public RenderData(TVertex[] vertices, uint[] indices, Shader shader,
            PrimitiveType primitiveType,
            PolygonMode polygonMode,
            MaterialFace materialFace)
        {
            PolygonMode = polygonMode;
            PrimitiveType = primitiveType;
            MaterialFace = materialFace;

            Shader = shader;
            VertexBuffer = new VertexBuffer<TVertex>(vertices);
            VertexDeclaration = VertexDeclarations.GetDeclaration<TVertex>(shader);
            IndexBuffer = new IndexBuffer(indices);
        }

        public void Draw()
        {
            VertexBuffer.Use();
            IndexBuffer.Use();
            VertexDeclaration.Use();

            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode);

            GL.DrawElements(PrimitiveType, IndexBuffer.Length, DrawElementsType.UnsignedInt, 0);
        }
    }
}
