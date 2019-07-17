using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.MonoGame.Forms.Drawables
{
    public class DrawableFactory
    {
        public ContentProvider ContentProvider { get; set; }

        public DrawableFactory(GameServiceContainer services)
        {
            ContentProvider = new ContentProvider(services);
        }

        public IDrawable CreatePointCloud(Domain.Vector3[] points, int radius)
        {
            var xnaPoints = points
                .Select(v => new Microsoft.Xna.Framework.Vector3(v.X, v.Y, v.Z))
                .ToArray();

            return new PointCloud(ContentProvider.PointEffect.Value, xnaPoints, radius);
        }
    }
}
