using GeometricAlgorithms.MonoGame.Shader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.MonoGame
{
    class PointCloud
    {
        private readonly GraphicsDevice Device;
        private VertexBuffer Vertices;
        private IndexBuffer Indices;

        public int PixelWidth { get; set; }

        public PointCloud(GraphicsDevice device, Vector3[] positions, int pixelWidth = 2)
        {
            PixelWidth = pixelWidth;

            Device = device;

            CreateBuffers(positions);
        }

        private void CreateBuffers(Vector3[] positions)
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

            Vertices = new VertexBuffer(Device, VertexPositionIndex.VertexDeclaration, vertices.Length, BufferUsage.None);
            Indices = new IndexBuffer(Device, IndexElementSize.ThirtyTwoBits, indices.Length, BufferUsage.None);

            Vertices.SetData(vertices);
            Indices.SetData(indices);
        }

        public void Draw(PointEffect effect)
        {
            Device.SetVertexBuffer(Vertices);
            Device.Indices = Indices;

            effect.PointPixels = PixelWidth;
            effect.ViewportWidth = Device.Viewport.Width;
            effect.ViewportHeight = Device.Viewport.Height;

            effect.DrawForEachPass(() => Device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, Indices.IndexCount / 3));
        }
    }

}
