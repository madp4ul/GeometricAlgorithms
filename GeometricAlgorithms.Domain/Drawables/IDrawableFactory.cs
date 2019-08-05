using System;
using System.Collections.Generic;
using System.Drawing;
using GeometricAlgorithms.Domain;

namespace GeometricAlgorithms.Domain.Drawables
{
    public delegate Vector3 VectorColorGenerator(Vector3 support, Vector3 direction);

    public interface IDrawableFactory
    {
        IDrawable CreateBoundingBoxRepresentation(BoundingBox[] boxes, Func<BoundingBox, Vector3> colorGenerator = null);
        IDrawable CreateHighlightedPointCloud(IEnumerable<Vector3> points, Vector3 highlightColor, int radius);
        IDrawable CreatePointCloud(IEnumerable<Vector3> points, int radius);
        IDrawable CreateWireframeSphere(float radius);
        IDrawable CreateVectors(
            IEnumerable<Vector3> supportVectors,
            IEnumerable<Vector3> directionVectors,
            float length,
            VectorColorGenerator colorGenerator = null);
    }
}