using GeometricAlgorithms.BusinessLogic.Extensions;
using GeometricAlgorithms.BusinessLogic.Model.FaceModels;
using GeometricAlgorithms.BusinessLogic.Model.KdTreeModels;
using GeometricAlgorithms.BusinessLogic.Model.NormalModels;
using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Drawables;
using GeometricAlgorithms.Domain.Tasks;
using GeometricAlgorithms.NormalOrientation;
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

        public readonly PositionModel Positions;
        public readonly KdTreeModel KdTree;
        public readonly NormalModel Normals;
        public readonly NormalApproximationModel ApproximatedNormals;
        public readonly FacesModel Faces;
        public readonly FaceApproximationModel ApproximatedFaces;

        public event Action Updated;

        public Workspace(IDrawableFactoryProvider drawableFactoryProvider, IFuncExecutor funcExecutor)
        {
            DrawableFactoryProvider = drawableFactoryProvider;

            ReferenceFrame = new ContainerDrawable();

            Positions = new PositionModel(drawableFactoryProvider);

            Normals = new NormalModel(drawableFactoryProvider);
            ApproximatedNormals = new NormalApproximationModel(drawableFactoryProvider, funcExecutor);
            Faces = new FacesModel(drawableFactoryProvider);
            KdTree = new KdTreeModels.KdTreeModel(drawableFactoryProvider, funcExecutor);
            ApproximatedFaces = new FaceApproximationModel(drawableFactoryProvider, funcExecutor);



            SetUpdateDependencies();
        }

        private void SetUpdateDependencies()
        {
            KdTree.Updated += () =>
            {
                var kdTree = KdTree.KdTree;

                ApproximatedFaces.Update(kdTree);
            };
        }

        public void Update(Mesh mesh)
        {
            Positions.Update(mesh);//pos
            Normals.Update(mesh);//norm
            ApproximatedNormals.Update(mesh);//pos/face
            Faces.Update(mesh);//face
            KdTree.Update(mesh);//pos

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
            foreach (var drawable in Positions.GetDrawables()
                .Concat(Normals.GetDrawables())
                .Concat(ApproximatedNormals.GetDrawables())
                .Concat(Faces.GetDrawables())
                .Concat(KdTree.GetDrawables())
                .Concat(ApproximatedFaces.GetDrawables())
                )
            {
                yield return drawable;
            }
        }
    }
}