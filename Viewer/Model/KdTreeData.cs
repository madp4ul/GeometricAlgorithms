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
    class KdTreeData
    {
        public ToggleableDrawable Drawable { get; set; }

        public KdTree<GenericVertex> KdTree { get; set; }
    }
}
