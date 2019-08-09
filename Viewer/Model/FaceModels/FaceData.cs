using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Drawables;
using GeometricAlgorithms.Viewer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Viewer.Model.FaceModels
{
    public class FaceData : ToggleableDrawable
    {
        private readonly IDrawableFactoryProvider DrawableFactoryProvider;

        public Mesh Mesh { get; private set; }

        public bool HasFaces { get; set; }

        public bool DrawAsWireframe { get; set; }
        IDrawableMesh DrawableMesh => Drawable as IDrawableMesh;


        public FaceData(IDrawableFactoryProvider drawableFactoryProvider)
        {
            DrawableFactoryProvider = drawableFactoryProvider ?? throw new ArgumentNullException(nameof(drawableFactoryProvider));

            DrawAsWireframe = true;
            EnableDraw = true;
        }

        public void Reset(Mesh mesh)
        {
            Mesh = mesh ?? throw new ArgumentNullException(nameof(mesh));
            HasFaces = MeshHasFaces(mesh);

            if (Drawable != null)
            {
                Drawable.Dispose();
            }

            if (HasFaces)
            {
                Drawable = DrawableFactoryProvider.DrawableFactory.CreateMesh(Mesh.Positions, SelectFaces(Mesh), GenerateColor);
            }
            else
            {
                Drawable = new EmptyDrawable();
            }
        }

        protected virtual IEnumerable<Triangle> SelectFaces(Mesh mesh)
        {
            return mesh.FileFaces;
        }

        protected virtual Vector3 GenerateColor(Vector3 position)
        {
            Vector3 n = position.Normalized();
            return new Vector3(Math.Abs(n.X), Math.Abs(n.Y), Math.Abs(n.Z));
        }

        protected bool MeshHasFaces(Mesh mesh)
        {
            return SelectFaces(mesh) != null;
        }

        public override void Draw(ACamera camera)
        {
            if (DrawableMesh != null)
            {
                DrawableMesh.DrawWireframe = this.DrawAsWireframe;
            }
            base.Draw(camera);
        }
    }
}
