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
        public readonly NormalOrientationModel NormalOrientation;
        public readonly FacesModel Faces;

        public readonly ImplicitSurfaceModel ImplicitSurface;
        public readonly FaceApproximationModel FaceApproximation;
        public readonly TreeFaceApproximationModel TreeFaceApproximation;
        public readonly FacesModel ApproximatedFaces;

        public event Action Updated;

        public Workspace(IDrawableFactoryProvider drawableFactoryProvider, IFuncExecutor funcExecutor)
        {
            DrawableFactoryProvider = drawableFactoryProvider;

            ReferenceFrame = new ContainerDrawable();

            Positions = new PositionModel(drawableFactoryProvider);

            Normals = new NormalModel(drawableFactoryProvider);
            ApproximatedNormals = new NormalApproximationModel(drawableFactoryProvider, funcExecutor);
            NormalOrientation = new NormalOrientationModel(funcExecutor);
            Faces = new FacesModel(drawableFactoryProvider);
            KdTree = new KdTreeModel(drawableFactoryProvider, funcExecutor);
            ImplicitSurface = new ImplicitSurfaceModel();
            FaceApproximation = new FaceApproximationModel(drawableFactoryProvider, funcExecutor);
            TreeFaceApproximation = new TreeFaceApproximationModel(drawableFactoryProvider, funcExecutor);
            ApproximatedFaces = new FacesModel(drawableFactoryProvider);

            SetUpdateDependencies();
        }

        private void SetUpdateDependencies()
        {
            KdTree.Updated += () =>
            {
                var kdTree = KdTree.Tree;

                ImplicitSurface.Update(kdTree);


                NormalOrientation.Update(kdTree);
            };

            ImplicitSurface.Updated += () =>
            {
                var surface = ImplicitSurface.ImplicitSurface;

                FaceApproximation.Update(surface);
                TreeFaceApproximation.Update(surface);
            };

            void updateApproximatedFaces(Mesh mesh) => ApproximatedFaces.Update(mesh);

            FaceApproximation.MeshCalculated += updateApproximatedFaces;
            TreeFaceApproximation.MeshCalculated += updateApproximatedFaces;
        }

        public void Update(Mesh mesh)
        {
            Positions.Update(mesh);//pos
            Normals.Update(mesh);//norm
            ApproximatedNormals.Update(mesh);//pos/face
            Faces.Update(mesh);//face
            KdTree.Update(mesh);//pos

            TreeFaceApproximation.Update(mesh);

            ApproximatedFaces.Update(Mesh.CreateEmpty());

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
                .Concat(FaceApproximation.GetDrawables())
                .Concat(TreeFaceApproximation.GetDrawables())
                .Concat(ApproximatedFaces.GetDrawables())
                )
            {
                yield return drawable;
            }
        }
    }
}