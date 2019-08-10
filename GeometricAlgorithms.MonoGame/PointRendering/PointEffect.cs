using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.MonoGame.PointRendering
{
    public class PointEffect : Effect, IViewProjectionEffect, IWorldEffect
    {
        private void SetWorldViewProjection(Matrix value)
        {
            Parameters["WorldViewProjection"].SetValue(value);
        }

        public int PointPixels
        {
            get { return Parameters["PointPixels"].GetValueInt32(); }
            set { Parameters["PointPixels"].SetValue(value); }
        }

        public int ViewportWidth
        {
            get { return Parameters["ViewportWidth"].GetValueInt32(); }
            set { Parameters["ViewportWidth"].SetValue(value); }
        }

        public int ViewportHeight
        {
            get { return Parameters["ViewportHeight"].GetValueInt32(); }
            set { Parameters["ViewportHeight"].SetValue(value); }
        }

        public Vector3 HighlightColor
        {
            get { return Parameters["HighlightColor"].GetValueVector3(); }
            set { Parameters["HighlightColor"].SetValue(value); }
        }

        private Matrix _ViewProjectionMatrix = Matrix.Identity;
        public Matrix ViewProjectionMatrix
        {
            get => _ViewProjectionMatrix;
            set
            {
                _ViewProjectionMatrix = value;
                SetWorldViewProjection(WorldMatrix * ViewProjectionMatrix);
            }
        }

        private Matrix _WorldMatrix = Matrix.Identity;
        public Matrix WorldMatrix
        {
            get => _WorldMatrix;
            set
            {
                _WorldMatrix = value;
                SetWorldViewProjection(WorldMatrix * ViewProjectionMatrix);
            }
        }

        protected PointEffect(Effect cloneSource) : base(cloneSource)
        {
        }

        public void ApplyPointDrawing()
        {
            Techniques["PointDrawing"].Passes["P0"].Apply();
        }

        public void ApplyPointHighlight()
        {
            Techniques["PointHighlight"].Passes["P0"].Apply();
        }

        public void ApplyPointColorDrawing()
        {
            Techniques["PointColorDrawing"].Passes["P0"].Apply();
        }

        public static PointEffect FromEffect(Effect effect)
        {
            return new PointEffect(effect);
        }
    }
}
