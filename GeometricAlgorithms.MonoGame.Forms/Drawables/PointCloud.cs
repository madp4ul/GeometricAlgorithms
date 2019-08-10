using GeometricAlgorithms.Domain.Drawables;
using GeometricAlgorithms.MonoGame.Forms.Extensions;
using GeometricAlgorithms.MonoGame.PointRendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.MonoGame.Forms.Drawables
{
    abstract class PointCloud<TVertex> : Domain.Drawables.IDrawable
         where TVertex : struct, IVertexType
    {
        private readonly Vector3[] Positions;

        protected VertexBuffer Vertices { get; private set; }
        protected IndexBuffer Indices { get; private set; }
        protected readonly PointEffect Effect;

        private GraphicsDevice Device { get { return Effect.GraphicsDevice; } }


        public int PixelWidth { get; set; }
        public Domain.Vector3 Position { get; set; }
        public Transformation Transformation { get; set; }

        public PointCloud(PointEffect effect, Vector3[] positions, int pixelWidth = 2)
        {
            if (positions.Length == 0)
            {
                throw new ArgumentException("Pointcloud needs at least 1 point");
            }

            Positions = positions;
            Transformation = new Transformation();
            Effect = effect;
            PixelWidth = pixelWidth;
        }

        protected void CreateBuffers()
        {
            {
                TVertex[] vertices = new TVertex[Positions.Length * 4];

                for (int positionIndex = 0; positionIndex < Positions.Length; positionIndex++)
                {
                    int vOffset = positionIndex * 4;

                    for (int cornerIndex = 0; cornerIndex < 4; cornerIndex++)
                    {
                        vertices[vOffset + cornerIndex] = CreateVertex(Positions[positionIndex], (Corner)cornerIndex, positionIndex);
                    }
                }
                Vertices = new VertexBuffer(Device, vertices[0].VertexDeclaration, vertices.Length, BufferUsage.None);
                Vertices.SetData<TVertex>(vertices);
            }

            {
                var indices = new int[Positions.Length * 6];

                for (int i = 0; i < Positions.Length; i++)
                {
                    int offset = i * 4;
                    indices[i * 6] = offset;
                    indices[i * 6 + 1] = offset + 1;
                    indices[i * 6 + 2] = offset + 2;

                    indices[i * 6 + 3] = offset + 2;
                    indices[i * 6 + 4] = offset + 3;
                    indices[i * 6 + 5] = offset + 0;
                }

                Indices = new IndexBuffer(Device, IndexElementSize.ThirtyTwoBits, indices.Length, BufferUsage.None);
                Indices.SetData(indices);
            }
        }

        protected abstract TVertex CreateVertex(Vector3 position, Corner corner, int pointIndex);

        protected virtual void ApplyEffectPass()
        {
            Effect.ApplyPointDrawing();
        }

        public virtual void Draw(Domain.ACamera camera)
        {
            Device.SetVertexBuffer(Vertices);
            Device.Indices = Indices;

            Effect.WorldMatrix = Transformation.GetWorldMatrix();
            Effect.ViewProjectionMatrix = camera.ViewProjection.ToXna();
            Effect.PointPixels = PixelWidth;
            Effect.ViewportWidth = Device.Viewport.Width;
            Effect.ViewportHeight = Device.Viewport.Height;

            ApplyEffectPass();
            Device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, Indices.IndexCount / 3);
        }

        public void Dispose()
        {
            Vertices.Dispose();
            Indices.Dispose();
            Effect.Dispose();
        }
    }
}
