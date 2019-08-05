using GeometricAlgorithms.Domain.Drawables;
using GeometricAlgorithms.MonoGame.Forms.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.MonoGame.Forms.Drawables
{
    class VectorsDrawable : Domain.Drawables.IDrawable
    {
        private readonly VertexBuffer Vertices;
        private readonly IndexBuffer Indices;
        private readonly BasicEffect Effect;

        private GraphicsDevice Device { get { return Effect.GraphicsDevice; } }

        public Transformation Transformation { get; set; }

        public VectorsDrawable(
            GraphicsDevice device,
            IEnumerable<Domain.Vector3> supportVectors,
            IEnumerable<Domain.Vector3> directionVectors,
            float length,
            VectorColorGenerator colorGenerator = null)
        {
            Transformation = new Transformation();

            Effect = new BasicEffect(device);

            if (colorGenerator == null)
            {
                colorGenerator = (s, d) => new Domain.Vector3(1, 1, 1);
            }

            var vertices = supportVectors.Zip(directionVectors, (sup, dir) => new { sup, dir })
                 .SelectMany(line =>
                 {
                     var endPoint = line.sup + line.dir.Normalized() * length;
                     Color color = new Color(colorGenerator(line.sup, line.dir).ToXna());
                     return new[] {
                         new VertexPositionColor(line.sup.ToXna(), color),
                         new VertexPositionColor(endPoint.ToXna(), color)
                     };
                 })
                 .ToArray();

            int[] indices = new int[vertices.Length];
            for (int i = 0; i < indices.Length; i++)
            {
                indices[i] = i;
            }

            Vertices = new VertexBuffer(Device, VertexPositionColor.VertexDeclaration, vertices.Length, BufferUsage.None);
            Vertices.SetData(vertices);

            Indices = new IndexBuffer(Device, IndexElementSize.ThirtyTwoBits, indices.Length, BufferUsage.None);
            Indices.SetData(indices);
        }

        public void Dispose()
        {
            Vertices.Dispose();
            Indices.Dispose();
            Effect.Dispose();
        }

        public void Draw(Domain.ACamera camera)
        {
            //Set buffers
            Device.SetVertexBuffer(Vertices);
            Device.Indices = Indices;

            //Set states
            //var prevDepthStencilState = Effect.GraphicsDevice.DepthStencilState;
            //Effect.GraphicsDevice.DepthStencilState = new DepthStencilState()
            //{
            //    DepthBufferEnable = false,
            //};

            //var prevBlendState = Effect.GraphicsDevice.BlendState;
            //Effect.GraphicsDevice.BlendState = BlendState.Additive;

            //set effect values
            Effect.World = Transformation.GetWorldMatrix();
            Effect.View = camera.ViewProjection.ToXna();
            Effect.Projection = Microsoft.Xna.Framework.Matrix.Identity;


            //Effect config and drawing
            Effect.VertexColorEnabled = true;

            Effect.CurrentTechnique.Passes[0].Apply();

            Device.DrawIndexedPrimitives(PrimitiveType.LineList, 0, 0, Indices.IndexCount / 2);

            //Reset states
            //Effect.GraphicsDevice.DepthStencilState = prevDepthStencilState;
            //Effect.GraphicsDevice.BlendState = prevBlendState;
        }
    }
}
