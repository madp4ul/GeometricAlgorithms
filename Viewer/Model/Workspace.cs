using GeometricAlgorithms.MonoGame.Forms.Cameras;
using GeometricAlgorithms.MonoGame.Forms.Drawables;
using GeometricAlgorithms.Viewer.Providers;

namespace GeometricAlgorithms.Viewer.Model
{
    public class Workspace : IDrawable
    {
        public readonly PointData PointData;


        public Workspace(IDrawableFactoryProvider drawableFactoryProvider)
        {
            PointData = new PointData(drawableFactoryProvider);
        }

        public Transformation Transformation { get => PointData.Transformation; set => PointData.Transformation = value; }

        public void Dispose()
        {
            PointData.Dispose();
        }

        public void Draw(ICamera camera)
        {
            PointData.Draw(camera);
        }
    }
}