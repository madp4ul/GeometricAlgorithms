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
    public class FaceData
    {
        protected readonly IDrawableFactoryProvider DrawableFactoryProvider;

        public Mesh Mesh { get; private set; }

        public bool HasFaces { get; private set; }

        private bool _DrawAsWireframe;
        public bool DrawAsWireframe
        {
            get => _DrawAsWireframe; set
            {
                _DrawAsWireframe = value;
                if (FacesDrawableMesh != null)
                {
                    FacesDrawableMesh.DrawWireframe = value;
                }
            }
        }
        IDrawableMesh FacesDrawableMesh => FacesDrawable.Drawable as IDrawableMesh;
        private readonly ContainerDrawable FacesDrawable;
        public bool DrawFaces { get => FacesDrawable.EnableDraw; set => FacesDrawable.EnableDraw = value; }


        public FaceData(IDrawableFactoryProvider drawableFactoryProvider)
        {
            DrawableFactoryProvider = drawableFactoryProvider ?? throw new ArgumentNullException(nameof(drawableFactoryProvider));

            FacesDrawable = new ContainerDrawable();

            DrawAsWireframe = true;
        }

        public void Reset(Mesh mesh)
        {
            Mesh = mesh ?? throw new ArgumentNullException(nameof(mesh));
            HasFaces = MeshHasFaces(mesh);

            IDrawable newDrawable;
            if (HasFaces)
            {
                var drawableMesh = DrawableFactoryProvider.DrawableFactory.CreateMesh(Mesh.Positions, SelectFaces(Mesh));
                drawableMesh.DrawWireframe = DrawAsWireframe;
                newDrawable = drawableMesh;
            }
            else
            {
                newDrawable = new EmptyDrawable();
            }
            FacesDrawable.SwapDrawable(newDrawable);
        }

        protected virtual IEnumerable<Triangle> SelectFaces(Mesh mesh)
        {
            return mesh.Faces;
        }

        protected bool MeshHasFaces(Mesh mesh)
        {
            var faces = SelectFaces(mesh);

            return faces != null && faces.Any();
        }

        public virtual IEnumerable<IDrawable> GetDrawables()
        {
            yield return FacesDrawable;
        }
    }
}
