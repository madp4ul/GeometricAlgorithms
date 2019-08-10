using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Drawables;
using GeometricAlgorithms.Viewer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Viewer.Model.NormalModels
{
    public class NormalData : ToggleableDrawable
    {
        private readonly IDrawableFactoryProvider DrawableFactoryProvider;

        public Mesh Mesh { get; private set; }

        public float Length { get; set; }

        public bool HasNormals { get; set; }

        public NormalData(IDrawableFactoryProvider drawableFactoryProvider)
        {
            DrawableFactoryProvider = drawableFactoryProvider ?? throw new ArgumentNullException(nameof(drawableFactoryProvider));

            Length = 0.02f;

            EnableDraw = true;
        }

        public void Reset()
        {
            HasNormals = MeshHasNormals(Mesh);

            if (Drawable != null)
            {
                Drawable.Dispose();
            }

            if (HasNormals)
            {
                Drawable = DrawableFactoryProvider.DrawableFactory.CreateVectors(
                     Mesh.Positions,
                     SelectNormals(Mesh),
                     Length,
                     GenerateColor);
            }
            else
            {
                Drawable = new EmptyDrawable();
            }
        }

        public void Reset(Mesh mesh)
        {
            Mesh = mesh ?? throw new ArgumentNullException(nameof(mesh));
            Reset();
        }

        protected virtual IEnumerable<Vector3> SelectNormals(Mesh mesh)
        {
            return mesh?.FileUnitNormals;
        }

        protected virtual Vector3 GenerateColor(Vector3 position, Vector3 direction)
        {
            Vector3 n = direction.Normalized();
            return new Vector3(Math.Abs(n.X), Math.Abs(n.Y), Math.Abs(n.Z));
        }

        protected bool MeshHasNormals(Mesh mesh)
        {
            return SelectNormals(mesh) != null;
        }
    }
}
