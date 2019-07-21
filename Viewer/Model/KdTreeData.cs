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

        public KdTreeData()
        {
            Drawable = new ToggleableDrawable();

            KdTree = new KdTree<GenericVertex>(new GenericVertex[0]);
        }

        public KdTreeData(Vector3[] points)
        {
            Reset(points);
        }

        public void Reset(Vector3[] points)
        {
            KdTree = new KdTree<GenericVertex>(points.Select(v => new GenericVertex(v)).ToArray());

            if (Drawable != null)
            {
                Drawable.Dispose();
            }
            Drawable = GeometricAlgorithmViewer.DrawableFactory.CreateBoundingBoxRepresentation(
                KdTree.GetBoundingBoxes().ToArray());
        }
    }
}
