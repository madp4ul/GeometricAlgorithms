using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.MonoGame.Shader
{
    class PointEffect : Effect
    {
        public Matrix WorldViewProjection
        {
            get { return Parameters["WorldViewProjection"].GetValueMatrix(); }
            set { Parameters["WorldViewProjection"].SetValue(value); }
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

        public PointEffect(GraphicsDevice graphicsDevice, byte[] effectCode) : base(graphicsDevice, effectCode)
        {
        }

        public PointEffect(GraphicsDevice graphicsDevice, byte[] effectCode, int index, int count) : base(graphicsDevice, effectCode, index, count)
        {
        }

        protected PointEffect(Effect cloneSource) : base(cloneSource)
        {
        }


        public void DrawForEachPass(Action draw)
        {
            foreach (var pass in Techniques["BasicColorDrawing"].Passes)
            {
                pass.Apply();

                draw();
            }
        }

        public static PointEffect FromEffect(Effect effect)
        {
            return new PointEffect(effect);
        }
    }
}
