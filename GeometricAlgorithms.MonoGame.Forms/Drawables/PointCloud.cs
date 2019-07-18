using GeometricAlgorithms.MonoGame.Forms.Cameras;
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
    class PointCloud : IDrawable
    {
        private readonly VertexBuffer Vertices;
        private readonly IndexBuffer Indices;
        private readonly PointEffect Effect;

        private GraphicsDevice Device { get { return Effect.GraphicsDevice; } }


        public int PixelWidth { get; set; }
        public Domain.Vector3 Position { get; set; }

        public PointCloud(PointEffect effect, Vector3[] positions, int pixelWidth = 2)
        {
            Effect = effect;
            PixelWidth = pixelWidth;

            {
                var vertices = new VertexPositionIndex[positions.Length * 4];

                for (int i = 0; i < positions.Length; i++)
                {
                    int vOffset = i * 4;

                    for (int j = 0; j < 4; j++)
                    {
                        vertices[vOffset + j] = new VertexPositionIndex(positions[i], (Corner)j);
                    }
                }
                Vertices = new VertexBuffer(Device, VertexPositionIndex.VertexDeclaration, vertices.Length, BufferUsage.None);
                Vertices.SetData(vertices);
            }

            {
                var indices = new int[positions.Length * 6];

                for (int i = 0; i < positions.Length; i++)
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

        int i = 0;
        public void Draw(ICamera camera)
        {
            Device.SetVertexBuffer(Vertices);
            Device.Indices = Indices;

            Effect.ViewProjectionMatrix = camera.Data.ViewProjectionMatrix;
            Effect.PointPixels = PixelWidth;
            Effect.ViewportWidth = Device.Viewport.Width;
            Effect.ViewportHeight = Device.Viewport.Height;

            ////        Matrix projection = Matrix.CreatePerspectiveFieldOfView(
            ////(float)Math.PI / 3,
            ////    1,
            ////0.0001f, 1000f);
            ////        Matrix view = Matrix.CreateLookAt(new Vector3(0, 1f, 3f),
            ////new Vector3(0.0f, 0.0f, 0.0f), Vector3.Up);
            //Matrix world = Matrix.CreateTranslation(new Vector3(-0.5f, -0.5f, 0));

            ////        Effect.ViewProjectionMatrix = view * projection;
            //Effect.WorldMatrix = world;

            //Matrix projection = Matrix.CreatePerspectiveFieldOfView((float)Math.PI / 3, 1, 0.0001f, 1000f);
            //Matrix view = Matrix.CreateLookAt(new Vector3((float)Math.Sin(i++), 1f, 3f),
            //    new Vector3(0.0f, 0.0f, 0.0f), Vector3.Up);

            //Effect.ViewProjectionMatrix = view * projection;

            Effect.DrawForEachPass(() => Device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, Indices.IndexCount / 3));
        }
    }
}
