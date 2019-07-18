using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.MonoGame.Forms.Drawables
{
    public class ToggleableDrawable : IDrawable
    {
        public readonly IDrawable ToToggle;

        public bool EnableDraw { get; set; }

        public ToggleableDrawable(IDrawable inner)
        {
            ToToggle = inner;
        }

        public void Draw()
        {
            if (EnableDraw)
            {
                ToToggle.Draw();
            }
        }
    }
}
