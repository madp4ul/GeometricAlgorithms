using GeometricAlgorithms.Domain.Cameras;
using GeometricAlgorithms.Domain.Drawables;
using GeometricAlgorithms.Domain.Tasks;
using GeometricAlgorithms.MonoGame.Forms.Cameras;
using GeometricAlgorithms.MonoGame.Forms.Drawables;
using GeometricAlgorithms.Viewer.Providers;

namespace GeometricAlgorithms.Viewer.Model
{
    public class Workspace : IDrawable
    {
        public readonly PointData PointData;


        public Workspace(IDrawableFactoryProvider drawableFactoryProvider, IFuncExecutor funcExecutor)
        {
            PointData = new PointData(drawableFactoryProvider, funcExecutor);
        }

        public Transformation Transformation { get => PointData.Transformation; set => PointData.Transformation = value; }

        public void Dispose()
        {
            PointData.Dispose();
        }

        public void Draw(ACamera camera)
        {
            PointData.Draw(camera);
        }
    }
}