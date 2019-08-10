using System;
using System.Collections.Generic;
using System.Drawing;
using GeometricAlgorithms.Domain;

namespace GeometricAlgorithms.Domain.Drawables
{
    public delegate Vector3 VectorColorGenerator(Vector3 support, Vector3 direction);
    public delegate Vector3 BoundingBoxColorGenerator(BoundingBox box);
    public delegate Vector3 PositionColorGenerator(Vector3 position);

    public interface IDrawableFactory
    {
        IDrawable CreateBoundingBoxRepresentation(BoundingBox[] boxes, BoundingBoxColorGenerator colorGenerator = null);
        IDrawable CreateHighlightedPointCloud(IEnumerable<Vector3> points, Vector3 highlightColor, int radius);
        IDrawable CreatePointCloud(IEnumerable<Vector3> points, int radius, IEnumerable<Vector3> customColors = null);
        IDrawableMesh CreateSphereMesh(float radius);
        IDrawable CreateVectors(
            IEnumerable<Vector3> supportVectors,
            IEnumerable<Vector3> directionVectors,
            float length,
            VectorColorGenerator colorGenerator = null);

        IDrawableMesh CreateMesh(IReadOnlyList<Vector3> vertices, IEnumerable<Triangle> faces);
    }
}