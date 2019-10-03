using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.Approximation
{
    class EditableIndexVertex
    {
        public readonly Vector3 Position;

        public int Index = -1;
        public bool HasIndex => Index != -1;

        public EditableIndexVertex(Vector3 position)
        {
            Position = position;
        }

        public override string ToString()
        {
            return $"{{editable index vertex: {Position}, {Index}}}";
        }
    }
}
