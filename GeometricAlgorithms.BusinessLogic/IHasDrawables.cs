using GeometricAlgorithms.Domain.Drawables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.BusinessLogic
{
    public interface IHasDrawables
    {
        IEnumerable<IDrawable> GetDrawables();
    }
}
