using GeometricAlgorithms.MonoGame.Forms.Drawables;
using GeometricAlgorithms.Viewer.Providers;

namespace GeometricAlgorithms.Viewer.Model
{
    public class Workspace
    {
        public readonly PointData PointData;


        public Workspace(IDrawableFactoryProvider drawableFactoryProvider)
        {
            PointData = new PointData(drawableFactoryProvider);
        }
    }
}