using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometricAlgorithms.Domain.Cameras;
using GeometricAlgorithms.Domain.Drawables;

namespace GeometricAlgorithms.Domain.Drawables
{
    public class ToggleableDrawable : IDrawable
    {
        public IDrawable Drawable { get; protected set; }

        public bool EnableDraw { get; set; }
        public virtual Transformation Transformation { get => Drawable.Transformation; set => Drawable.Transformation = value; }

        public ToggleableDrawable(bool enable = true)
            : this(new EmptyDrawable(), enable)
        {
        }

        public ToggleableDrawable(IDrawable inner, bool enable = true)
        {
            Drawable = inner;
            EnableDraw = enable;
        }

        public virtual void Draw(ACamera camera)
        {
            if (EnableDraw)
            {
                Drawable.Draw(camera);
            }
        }

        public void Dispose()
        {
            Drawable.Dispose();
        }
    }
}
