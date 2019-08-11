using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Domain.Drawables
{
    public class ContainerDrawable : ToggleableDrawable
    {
        public ContainerDrawable(bool enable = true) : base(enable)
        {
        }

        public ContainerDrawable(IDrawable inner, bool enable = true) : base(inner, enable)
        {
        }

        public void SwapDrawable(IDrawable newDrawable, bool disposeOld = true)
        {
            if (disposeOld)
            {
                Drawable.Dispose();
            }
            Drawable = newDrawable;
        }
    }
}
