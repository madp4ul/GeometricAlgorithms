using GeometricAlgorithms.Domain;
using GeometricAlgorithms.MonoGame.Forms.Cameras;
using GeometricAlgorithms.MonoGame.Forms.Drawables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Viewer.Model
{
    public class PointData : ToggleableDrawable
    {
        public Vector3[] Points { get; set; }

        public readonly KdTreeData KdTreeData;


        public PointData()
        {
            Points = new Vector3[0];
            Drawable = new EmptyDrawable();
            KdTreeData = new KdTreeData();
        }

        public PointData(Vector3[] points, int radius)
        {
            Reset(points, radius);
            KdTreeData = new KdTreeData(points);
        }

        public void Reset(Vector3[] points, int radius)
        {
            Points = points ?? throw new ArgumentNullException(nameof(points));

            if (Drawable != null)
            {
                Drawable.Dispose();
            }
            Drawable = GeometricAlgorithmViewer.DrawableFactory.CreatePointCloud(points, radius);

            KdTreeData.Reset(Points);
        }

    }
}
