using GeometricAlgorithms.BusinessLogic.Extensions;
using GeometricAlgorithms.BusinessLogic.Model.FaceModels;
using GeometricAlgorithms.BusinessLogic.Model.KdTreeModels;
using GeometricAlgorithms.BusinessLogic.Model.NormalModels;
using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Drawables;
using GeometricAlgorithms.Domain.Tasks;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GeometricAlgorithms.BusinessLogic.Model
{
    public class Workspace : IHasDrawables, IUpdatable<Mesh>
    {
        private readonly IDrawableFactoryProvider DrawableFactoryProvider;
        private readonly ContainerDrawable ReferenceFrame;

        public readonly PositionModel PointData;

        public readonly KdTreeModels.KdTreeModel KdTreeData;
        public readonly NormalModel NormalData;
        public readonly NormalApproximationModel ApproximatedNormalData;
        public readonly FacesModel FaceData;
        public readonly FaceApproximationModel ApproximatedFaceData;

        public readonly KdTreeRadiusQueryModel RadiusQuerydata;
        public readonly KdTreeNearestQueryModel NearestQuerydata;

        public event Action Updated;

        public Workspace(IDrawableFactoryProvider drawableFactoryProvider, IFuncExecutor funcExecutor)
        {
            DrawableFactoryProvider = drawableFactoryProvider;

            ReferenceFrame = new ContainerDrawable();

            PointData = new PositionModel(drawableFactoryProvider);

            NormalData = new NormalModel(drawableFactoryProvider);
            ApproximatedNormalData = new NormalApproximationModel(drawableFactoryProvider, funcExecutor);
            FaceData = new FacesModel(drawableFactoryProvider);
            KdTreeData = new KdTreeModels.KdTreeModel(drawableFactoryProvider, funcExecutor);
            ApproximatedFaceData = new FaceApproximationModel(drawableFactoryProvider, funcExecutor);

            RadiusQuerydata = new KdTreeRadiusQueryModel(drawableFactoryProvider, funcExecutor);
            NearestQuerydata = new KdTreeNearestQueryModel(drawableFactoryProvider, funcExecutor);

            SetUpdateDependencies();
        }

        private void SetUpdateDependencies()
        {
            //TODO make all depend directly on workspace
            PointData.Updated += () =>
            {
                var mesh = PointData.Mesh;

                NormalData.Update(mesh);
                ApproximatedNormalData.Update(mesh);
                FaceData.Update(mesh);
                KdTreeData.Update(mesh);
            };

            KdTreeData.Updated += () =>
            {
                var kdTree = KdTreeData.KdTree;

                ApproximatedFaceData.Update(kdTree);
                RadiusQuerydata.Update(kdTree);
                NearestQuerydata.Update(kdTree);
            };

        }

        public void Update(Mesh mesh)
        {
            //TODO use this
            PointData.Update(mesh);
            NormalData.Update(mesh);
            ApproximatedNormalData.Update(mesh);
            FaceData.Update(mesh);
            KdTreeData.Update(mesh);

            Updated?.Invoke();
        }

        public void LoadReferenceFrame()
        {
            var drawable = DrawableFactoryProvider.DrawableFactory.CreateBoundingBoxRepresentation(
                new[] { new BoundingBox(-Vector3.One, Vector3.One) }, (box) => Color.Blue.ToVector3());

            ReferenceFrame.SwapDrawable(drawable);
        }

        public IEnumerable<IDrawable> GetDrawables()
        {
            yield return ReferenceFrame;
            foreach (var drawable in PointData.GetDrawables()
                .Concat(NormalData.GetDrawables())
                .Concat(ApproximatedNormalData.GetDrawables())
                .Concat(FaceData.GetDrawables())
                .Concat(KdTreeData.GetDrawables())
                .Concat(ApproximatedFaceData.GetDrawables())
                .Concat(RadiusQuerydata.GetDrawables())
                .Concat(NearestQuerydata.GetDrawables())
                )
            {
                yield return drawable;
            }
        }
    }
}