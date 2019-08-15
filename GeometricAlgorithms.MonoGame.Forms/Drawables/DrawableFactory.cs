using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Drawables;
using GeometricAlgorithms.MonoGame.Forms.Extensions;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.MonoGame.Forms.Drawables
{
    public class DrawableFactory : IDrawableFactory
    {
        internal ContentProvider ContentProvider { get; set; }

        public DrawableFactory(Microsoft.Xna.Framework.GameServiceContainer services, GraphicsDevice device)
        {
            ContentProvider = new ContentProvider(services, device);
        }

        public IDrawable CreatePointCloud(IEnumerable<Vector3> points, int radius, IEnumerable<Vector3> customColors = null)
        {
            var xnaPoints = points
                .Select(v => v.ToXna())
                .ToArray();

            if (!xnaPoints.Any())
            {
                return new EmptyDrawable();
            }

            if (customColors == null)
            {
                return new PointCloudAutoColored(ContentProvider.PointEffect, xnaPoints, radius);
            }
            else
            {
                var xnaCustomColors = customColors.Select(v => v.ToXnaColor()).ToArray();

                return new PointCloudVertexColored(ContentProvider.PointEffect, xnaPoints, xnaCustomColors, radius);
            }
        }

        public IDrawable CreateBoundingBoxRepresentation(BoundingBox[] boxes, BoundingBoxColorGenerator colorGenerator = null)
        {
            if (boxes.Length == 0)
            {
                return new EmptyDrawable();
            }

            Func<BoundingBox, Microsoft.Xna.Framework.Vector3> xnaColorGenerator = null;
            if (colorGenerator != null)
            {
                xnaColorGenerator = (box) =>
                {
                    var c = colorGenerator(box);
                    return c.ToXna();
                };
            }

            return new BoundingBoxRepresentation(ContentProvider.GraphicsDevice, boxes, xnaColorGenerator);
        }

        public IDrawable CreateHighlightedPointCloud(IEnumerable<Vector3> points, Vector3 highlightColor, int radius)
        {
            var xnaPoints = points
                .Select(v => v.ToXna())
                .ToArray();

            if (!xnaPoints.Any())
            {
                return new EmptyDrawable();
            }

            return new HighlightedPointCloud(ContentProvider.PointEffect, xnaPoints, highlightColor.ToXna(), radius);
        }

        public IDrawableMesh CreateSphereMesh(float radius)
        {
            //TODO
            throw new NotImplementedException();
        }

        public IDrawable CreateVectors(
            IEnumerable<Vector3> supportVectors,
            IEnumerable<Vector3> directionVectors,
            float length,
            VectorColorGenerator colorGenerator = null)
        {
            return new VectorsDrawable(ContentProvider.GraphicsDevice, supportVectors, directionVectors, length, colorGenerator);
        }

        public IDrawableMesh CreateMesh(IReadOnlyList<Vector3> positions, IEnumerable<Triangle> faces)
        {
            return new FlatShadedMeshDrawable(ContentProvider.GraphicsDevice, positions, faces);
        }
    }
}
