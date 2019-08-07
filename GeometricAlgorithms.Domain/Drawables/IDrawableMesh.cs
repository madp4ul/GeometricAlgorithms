using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Domain.Drawables
{
    /// <summary>
    /// Represents a body with a solid surface
    /// </summary>
    public interface IDrawableMesh : IDrawable
    {
        bool DrawWireframe { get; set; }
    }
}
