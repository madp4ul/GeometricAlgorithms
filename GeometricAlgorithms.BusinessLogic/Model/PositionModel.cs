using GeometricAlgorithms.BusinessLogic.Model.FaceModels;
using GeometricAlgorithms.BusinessLogic.Model.KdTreeModels;
using GeometricAlgorithms.BusinessLogic.Model.NormalModels;
using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Drawables;
using GeometricAlgorithms.Domain.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.BusinessLogic.Model
{
    public class PositionModel : IHasDrawables, IUpdatable<Mesh>
    {
        private readonly IDrawableFactoryProvider DrawableFactoryProvider;

        public Mesh Mesh { get; private set; }

        private readonly ContainerDrawable MeshPositionDrawable;

        public event Action Updated;

        public bool DrawMeshPositions { get => MeshPositionDrawable.EnableDraw; set => MeshPositionDrawable.EnableDraw = value; }

        public int UsedPointRadius { get; private set; }

        public PositionModel(IDrawableFactoryProvider drawableFactoryProvider)
        {
            DrawableFactoryProvider = drawableFactoryProvider;

            UsedPointRadius = 2;
            Mesh = Mesh.CreateEmpty();
            MeshPositionDrawable = new ContainerDrawable();
        }

        public void Update(Mesh mesh)
        {
            Mesh = mesh ?? throw new ArgumentNullException(nameof(mesh));

            var pointCloud = DrawableFactoryProvider.DrawableFactory.CreatePointCloud(
                            Mesh.Positions, UsedPointRadius);
            MeshPositionDrawable.SwapDrawable(pointCloud);

            Updated?.Invoke();
        }

        public IEnumerable<IDrawable> GetDrawables()
        {
            yield return MeshPositionDrawable;
        }
    }
}
