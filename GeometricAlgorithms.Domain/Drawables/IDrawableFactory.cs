using System;
using System.Collections.Generic;
using System.Drawing;
using GeometricAlgorithms.Domain;

namespace GeometricAlgorithms.Domain.Drawables
{
    public interface IDrawableFactory
    {
        IDrawable CreateBoundingBoxRepresentation(BoundingBox[] boxes, Func<BoundingBox, Vector3> colorGenerator = null);
        IDrawable CreateHighlightedPointCloud(IEnumerable<Vector3> points, Vector3 highlightColor, int radius);
        IDrawable CreatePointCloud(IEnumerable<Vector3> points, int radius);
        IDrawable CreateWireframeSphere(float radius);
    }
}