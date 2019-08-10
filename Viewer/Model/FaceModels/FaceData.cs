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
        protected readonly IDrawableFactoryProvider DrawableFactoryProvider;

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
                Drawable = DrawableFactoryProvider.DrawableFactory.CreateMesh(Mesh.Positions, SelectFaces(Mesh));
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
