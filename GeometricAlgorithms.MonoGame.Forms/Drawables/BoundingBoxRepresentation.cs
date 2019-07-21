﻿using System;
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

            boxes = new BoundingBox[] {
                new BoundingBox(Vector3.One*1,Vector3.One*2),
                new BoundingBox(Vector3.One*3,Vector3.One*4),
                new BoundingBox(Vector3.One*5,Vector3.One*6),
            };

            const int pointsPerBox = 8;

            {
                var vertices = new VertexPositionColor[boxes.Length * pointsPerBox];

                for (int i = 0; i < boxes.Length; i++)
                {
                    int vOffset = i * pointsPerBox;
                    BoundingBox box = boxes[i];

                    Vector3 diff = box.Maximum - box.Minimum;

                    vertices[vOffset + 0] = new VertexPositionColor(box.Minimum.ToXna(), Color);
                    vertices[vOffset + 1] = new VertexPositionColor((box.Minimum + diff.ComponentX()).ToXna(), Color);
                    vertices[vOffset + 2] = new VertexPositionColor((box.Minimum + diff.ComponentY()).ToXna(), Color);
                    vertices[vOffset + 3] = new VertexPositionColor((box.Minimum + diff.ComponentZ()).ToXna(), Color);

                    vertices[vOffset + 4] = new VertexPositionColor(box.Maximum.ToXna(), Color);
                    vertices[vOffset + 5] = new VertexPositionColor((box.Maximum - diff.ComponentX()).ToXna(), Color);
                    vertices[vOffset + 6] = new VertexPositionColor((box.Maximum - diff.ComponentY()).ToXna(), Color);
                    vertices[vOffset + 7] = new VertexPositionColor((box.Maximum - diff.ComponentZ()).ToXna(), Color);

                }
                Vertices = new VertexBuffer(Device, VertexPositionColor.VertexDeclaration, vertices.Length, BufferUsage.None);
                Vertices.SetData(vertices);
            }

            {
                const int linesPerBox = 12;
                const int indicesPerBox = linesPerBox * 2;
                var indices = new int[boxes.Length * indicesPerBox];

                for (int i = 0; i < boxes.Length; i++)
                {
                    int index = 0;
                    int indexOffset = i * indicesPerBox;
                    int vertexOffset = i * pointsPerBox;
                    indices[indexOffset + index++] = vertexOffset;
                    indices[indexOffset + index++] = vertexOffset + 1;

                    indices[indexOffset + index++] = vertexOffset;
                    indices[indexOffset + index++] = vertexOffset + 2;

                    indices[indexOffset + index++] = vertexOffset;
                    indices[indexOffset + index++] = vertexOffset + 3;

                    indices[indexOffset + index++] = vertexOffset + 4;
                    indices[indexOffset + index++] = vertexOffset + 5;

                    indices[indexOffset + index++] = vertexOffset + 4;
                    indices[indexOffset + index++] = vertexOffset + 6;

                    indices[indexOffset + index++] = vertexOffset + 4;
                    indices[indexOffset + index++] = vertexOffset + 7; //  6/12

                    indices[indexOffset + index++] = vertexOffset + 1;
                    indices[indexOffset + index++] = vertexOffset + 6;

                    indices[indexOffset + index++] = vertexOffset + 1;
                    indices[indexOffset + index++] = vertexOffset + 7;

                    indices[indexOffset + index++] = vertexOffset + 2;
                    indices[indexOffset + index++] = vertexOffset + 5;

                    indices[indexOffset + index++] = vertexOffset + 2;
                    indices[indexOffset + index++] = vertexOffset + 7;

                    indices[indexOffset + index++] = vertexOffset + 3;
                    indices[indexOffset + index++] = vertexOffset + 5;

                    indices[indexOffset + index++] = vertexOffset + 3;
                    indices[indexOffset + index++] = vertexOffset + 6;
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

            Effect.VertexColorEnabled = true;

            Effect.CurrentTechnique.Passes[0].Apply();

            Device.DrawIndexedPrimitives(PrimitiveType.LineList, 0, 0, Indices.IndexCount / 2);
        }

        public void Dispose()
        {
            Vertices.Dispose();
            Indices.Dispose();
        }
    }
}
