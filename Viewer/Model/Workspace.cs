using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Cameras;
using GeometricAlgorithms.Domain.Drawables;
using GeometricAlgorithms.Domain.Tasks;
using GeometricAlgorithms.MonoGame.Forms.Cameras;
using GeometricAlgorithms.MonoGame.Forms.Drawables;
using GeometricAlgorithms.Viewer.Extensions;
using GeometricAlgorithms.Viewer.Interfaces;
using System.Drawing;

namespace GeometricAlgorithms.Viewer.Model
{
    public class Workspace : IDrawable
    {
        private readonly IDrawableFactoryProvider DrawableFactoryProvider;
        public readonly PointData PointData;

        private IDrawable ReferenceFrame;

        public Workspace(IDrawableFactoryProvider drawableFactoryProvider, IFuncExecutor funcExecutor)
        {
            DrawableFactoryProvider = drawableFactoryProvider;

            ReferenceFrame = new EmptyDrawable();
            PointData = new PointData(drawableFactoryProvider, funcExecutor);
        }

        public Transformation Transformation { get => PointData.Transformation; set => PointData.Transformation = value; }

        public void LoadReferenceFrame()
        {
            ReferenceFrame = DrawableFactoryProvider.DrawableFactory.CreateBoundingBoxRepresentation(
                new[] { new BoundingBox(-Vector3.One, Vector3.One) }, (box) => Color.Blue.ToVector3());
        }

        public void Dispose()
        {
            ReferenceFrame.Dispose();
            PointData.Dispose();
        }

        public void Draw(ACamera camera)
        {
            PointData.Draw(camera);
            ReferenceFrame.Draw(camera);
        }
    }
}