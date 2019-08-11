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
    public class NormalData
    {
        private readonly IDrawableFactoryProvider DrawableFactoryProvider;

        public Mesh Mesh { get; private set; }

        public float Length { get; set; }

        public bool HasNormals { get; set; }

        private readonly ContainerDrawable NormalsDrawable;
        public bool DrawNormals { get => NormalsDrawable.EnableDraw; set => NormalsDrawable.EnableDraw = value; }


        public NormalData(IDrawableFactoryProvider drawableFactoryProvider)
        {
            DrawableFactoryProvider = drawableFactoryProvider ?? throw new ArgumentNullException(nameof(drawableFactoryProvider));

            Length = 0.02f;
            NormalsDrawable = new ContainerDrawable();
        }

        public void Reset(Mesh mesh)
        {
            Mesh = mesh ?? throw new ArgumentNullException(nameof(mesh));
            Reset();
        }

        public void Reset()
        {
            HasNormals = MeshHasNormals(Mesh);

            var normals = !HasNormals
                ? new EmptyDrawable()
                : DrawableFactoryProvider.DrawableFactory.CreateVectors(
                     Mesh.Positions,
                     SelectNormals(Mesh),
                     Length,
                     GenerateColor);

            NormalsDrawable.SwapDrawable(normals);
        }

        protected virtual IEnumerable<Vector3> SelectNormals(Mesh mesh)
        {
            return mesh?.UnitNormals;
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

        public IEnumerable<IDrawable> GetDrawables()
        {
            yield return NormalsDrawable;
        }
    }
}
