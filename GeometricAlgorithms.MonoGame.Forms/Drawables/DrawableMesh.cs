using GeometricAlgorithms.Domain.Drawables;
using GeometricAlgorithms.MonoGame.Forms.Extensions;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.MonoGame.Forms.Drawables
{
    class DrawableMesh : IDrawableMesh
    {
        private readonly VertexBuffer Vertices;
        private readonly IndexBuffer Indices;
        private readonly BasicEffect Effect;

        private GraphicsDevice Device { get { return Effect.GraphicsDevice; } }

        public bool DrawWireframe { get; set; }
        public Transformation Transformation { get; set; }

        public DrawableMesh(
           GraphicsDevice device,
           IEnumerable<Domain.Vector3> positions,
           IEnumerable<Domain.IFace> faces,
           Func<Domain.Vector3, Microsoft.Xna.Framework.Vector3> colorGenerator = null)
        {
            Effect = new BasicEffect(device);
            Transformation = new Transformation();

            if (colorGenerator == null)
            {
                colorGenerator = (pos) => Microsoft.Xna.Framework.Color.OrangeRed.ToVector3();
            }

            //Vertexbuffer
            {
                var vertices = positions
                    .Select(p => new VertexPositionColor(p.ToXna(), new Microsoft.Xna.Framework.Color(colorGenerator(p))))
                    .ToArray();

                Vertices = new VertexBuffer(Device, VertexPositionColor.VertexDeclaration, vertices.Length, BufferUsage.None);
                Vertices.SetData(vertices);
            }

            //Indexbuffer
            {
                var indices = faces.SelectMany((f, i) =>
                    {
                        var faceIndices = f.ToArray();
                        if (faceIndices.Length != 3)
                        {
                            throw new NotImplementedException("Only support faces out of with 3 points");
                        }
                        return faceIndices;
                    })
                    .ToArray();


                Indices = new IndexBuffer(Device, IndexElementSize.ThirtyTwoBits, indices.Length, BufferUsage.None);
                Indices.SetData(indices);
            }
        }

        public void Draw(Domain.ACamera camera)
        {
            //Set buffers
            Device.SetVertexBuffer(Vertices);
            Device.Indices = Indices;

            //Set states
            var prevRasterizerState = Device.RasterizerState;
            Effect.GraphicsDevice.RasterizerState = new RasterizerState()
            {
                FillMode = DrawWireframe ? FillMode.WireFrame : FillMode.Solid,
                CullMode = CullMode.None,
            };

            //set effect values
            Effect.World = Transformation.GetWorldMatrix();
            Effect.View = camera.ViewProjection.ToXna();
            Effect.Projection = Microsoft.Xna.Framework.Matrix.Identity;

            //Effect config and drawing
            Effect.VertexColorEnabled = true;

            Effect.CurrentTechnique.Passes[0].Apply();

            Device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, Indices.IndexCount / 2);

            //Reset states
            Effect.GraphicsDevice.RasterizerState = prevRasterizerState;
        }

        public void Dispose()
        {
            Vertices.Dispose();
            Indices.Dispose();
            Effect.Dispose();
        }
    }
}
