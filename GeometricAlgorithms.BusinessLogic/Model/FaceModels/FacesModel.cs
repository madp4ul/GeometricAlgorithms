using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Drawables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.BusinessLogic.Model.FaceModels
{
    public class FacesModel : IHasDrawables, IUpdatable<Mesh>
    {
        protected readonly IDrawableFactoryProvider DrawableFactoryProvider;

        public Mesh Mesh { get; private set; }

        public bool HasFaces => Mesh.HasFaces;

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

        public event Action Updated;

        public bool DrawFaces { get => FacesDrawable.EnableDraw; set => FacesDrawable.EnableDraw = value; }


        public FacesModel(IDrawableFactoryProvider drawableFactoryProvider)
        {
            DrawableFactoryProvider = drawableFactoryProvider ?? throw new ArgumentNullException(nameof(drawableFactoryProvider));

            FacesDrawable = new ContainerDrawable();

            DrawAsWireframe = true;
        }

        public void Update(Mesh mesh)
        {
            Mesh = mesh ?? throw new ArgumentNullException(nameof(mesh));

            IDrawable newDrawable;
            if (Mesh.HasFaces)
            {
                var drawableMesh = DrawableFactoryProvider.DrawableFactory.CreateMesh(Mesh.Positions, mesh.Faces);
                drawableMesh.DrawWireframe = DrawAsWireframe;
                newDrawable = drawableMesh;
            }
            else
            {
                newDrawable = new EmptyDrawable();
            }
            FacesDrawable.SwapDrawable(newDrawable);

            Updated?.Invoke();
        }

        public virtual IEnumerable<IDrawable> GetDrawables()
        {
            yield return FacesDrawable;
        }
    }
}
