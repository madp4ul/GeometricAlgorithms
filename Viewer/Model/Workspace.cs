using GeometricAlgorithms.MonoGame.Forms.Drawables;

namespace GeometricAlgorithms.Viewer.Model
{
    public class Workspace
    {
        public ToggleableDrawable PointCloud { get; private set; }

        public Workspace()
        {
            PointCloud = new ToggleableDrawable(new EmptyDrawable());
        }
    }
}