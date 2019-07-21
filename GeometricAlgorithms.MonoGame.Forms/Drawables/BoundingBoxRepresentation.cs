using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometricAlgorithms.Domain;
using GeometricAlgorithms.MonoGame.Forms.Cameras;
using GeometricAlgorithms.MonoGame.Forms.Extensions;
using Microsoft.Xna.Framework.Graphics;

namespace GeometricAlgorithms.MonoGame.Forms.Drawables
{
    class BoundingBoxRepresentation : IDrawable
    {
        public static Microsoft.Xna.Framework.Color Color = Microsoft.Xna.Framework.Color.OrangeRed;

        private readonly VertexBuffer Vertices;
        private readonly IndexBuffer Indices;
        private readonly BasicEffect Effect;

        private GraphicsDevice Device { get { return Effect.GraphicsDevice; } }

        public Transformation Transformation { get; set; }

        public BoundingBoxRepresentation(GraphicsDevice device, BoundingBox[] boxes)
        {
            Effect = new BasicEffect(device);
            Transformation = new Transformation();

            const int PointsPerBox = 8;

            {
                var vertices = new VertexPositionColor[boxes.Length * PointsPerBox];

                for (int i = 0; i < boxes.Length; i++)
                {
                    int vOffset = i * PointsPerBox;
                    BoundingBox box = boxes[i];

                    Vector3 diff = box.Maximum - box.Minimum;

                    vertices[vOffset + 0] = new VertexPositionColor(box.Minimum.ToXna(), Color);
                    vertices[vOffset + 1] = new VertexPositionColor((box.Minimum + diff.ComponentX()).ToXna(), Color);
                    vertices[vOffset + 2] = new VertexPositionColor((box.Minimum + diff.ComponentY()).ToXna(), Color);
                    vertices[vOffset + 3] = new VertexPositionColor((box.Minimum + diff.ComponentZ()).ToXna(), Color);

                    vertices[vOffset + 4] = new VertexPositionColor(box.Maximum.ToXna(), Color);
                    vertices[vOffset + 5] = new VertexPositionColor((box.Minimum - diff.ComponentX()).ToXna(), Color);
                    vertices[vOffset + 6] = new VertexPositionColor((box.Minimum - diff.ComponentY()).ToXna(), Color);
                    vertices[vOffset + 7] = new VertexPositionColor((box.Minimum - diff.ComponentZ()).ToXna(), Color);

                }
                Vertices = new VertexBuffer(Device, VertexPositionColor.VertexDeclaration, vertices.Length, BufferUsage.None);
                Vertices.SetData(vertices);
            }

            {
                const int LinesPerBox = 12;
                const int IndicesPerBox = LinesPerBox * 2;
                var indices = new int[boxes.Length * IndicesPerBox];

                for (int i = 0; i < boxes.Length; i++)
                {
                    int index = 0;
                    int offset = i * PointsPerBox;
                    indices[i * LinesPerBox + index++] = offset;
                    indices[i * LinesPerBox + index++] = offset + 1;

                    indices[i * LinesPerBox + index++] = offset;
                    indices[i * LinesPerBox + index++] = offset + 2;

                    indices[i * LinesPerBox + index++] = offset;
                    indices[i * LinesPerBox + index++] = offset + 3;

                    indices[i * LinesPerBox + index++] = offset + 4;
                    indices[i * LinesPerBox + index++] = offset + 5;

                    indices[i * LinesPerBox + index++] = offset + 4;
                    indices[i * LinesPerBox + index++] = offset + 6;

                    indices[i * LinesPerBox + index++] = offset + 4;
                    indices[i * LinesPerBox + index++] = offset + 7; //  6/12
                }

                Indices = new IndexBuffer(Device, IndexElementSize.ThirtyTwoBits, indices.Length, BufferUsage.None);
                Indices.SetData(indices);
            }
        }

        public void Draw(ICamera camera)
        {
            Device.SetVertexBuffer(Vertices);
            Device.Indices = Indices;

            Effect.World = Transformation.GetWorldMatrix();
            Effect.View = camera.Data.ViewProjectionMatrix;
            Effect.Projection = Microsoft.Xna.Framework.Matrix.Identity;

            Device.DrawIndexedPrimitives(PrimitiveType.LineList, 0, 0, Indices.IndexCount / 2);
        }

        public void Dispose()
        {
            Vertices.Dispose();
            Indices.Dispose();
            Effect.Dispose();
        }
    }
}
