using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.VertexTypes;
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
        public GenericVertex[] Points { get; set; }

        public readonly KdTreeData KdTreeData;


        public PointData()
        {
            Points = new GenericVertex[0];
            Drawable = new EmptyDrawable();
            KdTreeData = new KdTreeData();
        }

        public PointData(GenericVertex[] points, int radius)
        {
            Reset(points, radius);
            KdTreeData = new KdTreeData(points);
        }

        public void Reset(GenericVertex[] points, int radius)
        {
            Points = points ?? throw new ArgumentNullException(nameof(points));

            if (Drawable != null)
            {
                Drawable.Dispose();
            }
            Drawable = GeometricAlgorithmViewer.DrawableFactory.CreatePointCloud(points.Select(v => v.Position).ToArray(), radius);

            KdTreeData.Reset(Points);
        }

    }
}
