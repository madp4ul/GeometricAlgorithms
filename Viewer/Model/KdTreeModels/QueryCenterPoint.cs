﻿using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Drawables;
using GeometricAlgorithms.Viewer.Extensions;
using GeometricAlgorithms.Viewer.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Viewer.Model.KdTreeModels
{
    public class QueryCenterPoint
    {
        readonly IDrawableFactoryProvider DrawableFactoryProvider;

        private readonly ContainerDrawable QueryCenterDrawable;

        public bool Show { get => QueryCenterDrawable.EnableDraw; set => QueryCenterDrawable.EnableDraw = value; }

        public QueryCenterPoint(IDrawableFactoryProvider drawableFactoryProvider)
        {
            DrawableFactoryProvider = drawableFactoryProvider ?? throw new ArgumentNullException(nameof(drawableFactoryProvider));
            QueryCenterDrawable = new ContainerDrawable(enable: false);
        }

        public void Reset()
        {
            var drawable = DrawableFactoryProvider.DrawableFactory.CreateHighlightedPointCloud(
                points: new[] { Vector3.Zero },
                highlightColor: Color.Red.ToVector3(),
                radius: 10);

            QueryCenterDrawable.SwapDrawable(drawable);
        }

        public void SetPosition(Vector3 position)
        {
            QueryCenterDrawable.Transformation.Position = position;
        }

        public IEnumerable<IDrawable> GetDrawables()
        {
            yield return QueryCenterDrawable;
        }
    }
}
