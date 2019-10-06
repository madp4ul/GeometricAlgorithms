using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.Approximation
{
    class EdgeIntersection : IDisposable
    {
        private readonly LinkedListNode<EditableIndexVertex> Intersection;

        public EditableIndexVertex VertexIndex => !IsDisposed ? Intersection.Value : throw new InvalidOperationException();

        public bool IsDisposed { get; private set; }

        public EdgeIntersection(LinkedListNode<EditableIndexVertex> intersection)
        {
            Intersection = intersection ?? throw new ArgumentNullException(nameof(intersection));

            IsDisposed = false;
        }

        public override string ToString()
        {
            return $"{{edge intersection: {Intersection?.Value}}}";
        }

        public void Dispose()
        {
            if (IsDisposed)
            {
                throw new InvalidOperationException();
            }

            Intersection.List.Remove(Intersection);

            IsDisposed = true;
        }
    }
}
