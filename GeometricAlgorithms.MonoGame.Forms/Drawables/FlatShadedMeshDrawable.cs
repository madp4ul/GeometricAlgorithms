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
    class FlatShadedMeshDrawable : IDrawableMesh
    {
        private readonly VertexBuffer Vertices;
        private readonly IndexBuffer Indices;
        private readonly BasicEffect Effect;

        private GraphicsDevice Device { get { return Effect.GraphicsDevice; } }

        public bool DrawWireframe { get; set; }
        public Transformation Transformation { get; set; }

        public FlatShadedMeshDrawable(
           GraphicsDevice device,
           IReadOnlyList<Domain.Vector3> positions,
           IEnumerable<Domain.Triangle> faces)
        {
            Effect = new BasicEffect(device);
            Transformation = new Transformation();

            //Vertexbuffer
            {
                var vertices = faces
                     .Select(f => new TriangleNormal(f, positions))
                     .SelectMany(tn => new[]
                     {
                    new VertexPositionNormalTexture(tn.Corner1, tn.Normal, Vector2.Zero),
                    new VertexPositionNormalTexture(tn.Corner2, tn.Normal, Vector2.Zero),
                    new VertexPositionNormalTexture(tn.Corner3, tn.Normal, Vector2.Zero),
                     })
                     .ToArray();

                Vertices = new VertexBuffer(Device, VertexPositionNormalTexture.VertexDeclaration, vertices.Length, BufferUsage.None);
                Vertices.SetData(vertices);
            }

            //Indexbuffer
            {
                var indices = Enumerable.Range(0, Vertices.VertexCount).ToArray();


                Indices = new IndexBuffer(Device, IndexElementSize.ThirtyTwoBits, indices.Length, BufferUsage.None);
                Indices.SetData(indices);
            }

            SetLighting();
        }

        public void Draw(Domain.ACamera camera)
        {
            //Set buffers
            Device.SetVertexBuffer(Vertices);
            Device.Indices = Indices;

            //Set states
            var prevRasterizerState = Device.RasterizerState;
            Device.RasterizerState = new RasterizerState()
            {
                FillMode = DrawWireframe ? FillMode.WireFrame : FillMode.Solid,
                CullMode = CullMode.None,
            };

            //set effect values
            Effect.World = Transformation.GetWorldMatrix();
            Effect.View = camera.ViewProjection.ToXna();
            Effect.Projection = Microsoft.Xna.Framework.Matrix.Identity;

            //Effect config and drawing
            Effect.CurrentTechnique.Passes[0].Apply();

            Device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, Indices.IndexCount / 2);

            //Reset states
            Device.RasterizerState = prevRasterizerState;
        }

        public void Dispose()
        {
            Vertices.Dispose();
            Indices.Dispose();
            Effect.Dispose();
        }

        private void SetLighting()
        {
            Effect.LightingEnabled = true;

            // Key light.
            Effect.DirectionalLight0.Direction = new Vector3(-0.5265408f, -0.5735765f, -0.6275069f);
            Effect.DirectionalLight0.DiffuseColor = new Vector3(1, 0.9607844f, 0.8078432f) * 0.7f;
            Effect.DirectionalLight0.SpecularColor = new Vector3(1, 0.9607844f, 0.8078432f) * 0.2f;
            Effect.DirectionalLight0.Enabled = true;

            // Fill light.
            Effect.DirectionalLight1.Direction = new Vector3(0.7198464f, 0.3420201f, 0.6040227f);
            Effect.DirectionalLight1.DiffuseColor = new Vector3(0.9647059f, 0.7607844f, 0.4078432f) * 0.7f;
            Effect.DirectionalLight1.SpecularColor = Vector3.Zero;
            Effect.DirectionalLight1.Enabled = true;

            // Back light.
            Effect.DirectionalLight2.Direction = new Vector3(0.4545195f, -0.7660444f, 0.4545195f);
            Effect.DirectionalLight2.DiffuseColor = new Vector3(0.3231373f, 0.3607844f, 0.3937255f) * 0.7f;
            Effect.DirectionalLight2.SpecularColor = new Vector3(0.3231373f, 0.3607844f, 0.3937255f) * 0.7f;
            Effect.DirectionalLight2.Enabled = true;

            // Ambient light.
            Effect.AmbientLightColor = new Vector3(0.05333332f, 0.09882354f, 0.1819608f);
        }

        private class TriangleNormal
        {
            public Domain.Triangle Triangle { get; set; }
            public Vector3 Corner1 { get; set; }
            public Vector3 Corner2 { get; set; }
            public Vector3 Corner3 { get; set; }
            public Vector3 Normal { get; set; }

            public TriangleNormal(Domain.Triangle triangle, IReadOnlyList<Domain.Vector3> positions)
            {
                Triangle = triangle;
                Corner1 = positions[triangle.Index0].ToXna();
                Corner2 = positions[triangle.Index1].ToXna();
                Corner3 = positions[triangle.Index2].ToXna();

                Normal = Vector3.Cross(Corner2 - Corner1, Corner3 - Corner1);
                Normal.Normalize();
            }
        }
    }
}
