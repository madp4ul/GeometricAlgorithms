using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometricAlgorithms.MonoGame.Forms.Cameras;

namespace GeometricAlgorithms.MonoGame.Forms.Drawables
{
    public class ToggleableDrawable : IDrawable
    {
        public IDrawable Drawable { get; set; }

        public bool EnableDraw { get; set; }

        public ToggleableDrawable(IDrawable inner)
        {
            Drawable = inner;
            EnableDraw = true;
        }

        public void Draw(ICamera camera)
        {
            if (EnableDraw)
            {
                Drawable.Draw(camera);
            }
        }
    }
}
