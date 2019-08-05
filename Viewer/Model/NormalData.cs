using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Drawables;
using GeometricAlgorithms.Viewer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Viewer.Model
{
    public class NormalData : ToggleableDrawable
    {
        private readonly IDrawableFactoryProvider DrawableFactoryProvider;

        public Mesh<VertexNormal> Mesh { get; private set; }

        public float Length { get; set; }

        public bool HasNormals { get; set; }

        public NormalData(IDrawableFactoryProvider drawableFactoryProvider)
        {
            DrawableFactoryProvider = drawableFactoryProvider ?? throw new ArgumentNullException(nameof(drawableFactoryProvider));

            Length = 0.02f;

            EnableDraw = true;
        }

        public void Reset(Mesh<VertexNormal> mesh)
        {
            Mesh = mesh ?? throw new ArgumentNullException(nameof(mesh));
            HasNormals = MeshHasNormals(mesh);

            if (Drawable != null)
            {
                Drawable.Dispose();
            }

            if (HasNormals)
            {
                Drawable = DrawableFactoryProvider.DrawableFactory.CreateVectors(
                     Mesh.Vertices.Select(v => v.Position),
                     Mesh.Vertices.Select(v => SelectNormal(v).Value),
                     Length,
                     GenerateColor);
            }
            else
            {
                Drawable = new EmptyDrawable();
            }
        }

        protected virtual Vector3? SelectNormal(VertexNormal vertexNormal)
        {
            return vertexNormal.OriginalNormal;
        }

        protected virtual Vector3 GenerateColor(Vector3 position, Vector3 direction)
        {
            Vector3 n = direction.Normalized();
            return new Vector3(Math.Abs(n.X), Math.Abs(n.Y), Math.Abs(n.Z));
        }

        protected bool MeshHasNormals(Mesh<VertexNormal> mesh)
        {
            return mesh.Vertices.Length > 0 && SelectNormal(mesh.Vertices[0]).HasValue;
        }
    }
}
