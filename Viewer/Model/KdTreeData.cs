using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.VertexTypes;
using GeometricAlgorithms.KdTree;
using GeometricAlgorithms.MonoGame.Forms.Drawables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Viewer.Model
{
    public class KdTreeData : ToggleableDrawable
    {
        public KdTree<GenericVertex> KdTree { get; set; }
        public KdTreeConfiguration Configuration { get; set; }

        public KdTreeData()
        {
            Drawable = new ToggleableDrawable();

            Configuration = KdTreeConfiguration.Default;
            KdTree = new KdTree<GenericVertex>(new GenericVertex[0], Configuration);
        }

        public KdTreeData(Vector3[] points)
        {
            Configuration = KdTreeConfiguration.Default;
            Reset(points);
        }

        public void Reset(Vector3[] points)
        {
            Configuration.MaximumPointsPerLeaf = 50;
            KdTree = new KdTree<GenericVertex>(points.Select(v => new GenericVertex(v)).ToArray(), Configuration);

            if (Drawable != null)
            {
                Drawable.Dispose();
            }
            Drawable = GeometricAlgorithmViewer.DrawableFactory.CreateBoundingBoxRepresentation(
                KdTree.GetBoundingBoxes().ToArray());
        }
    }
}
