using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.Approximation
{
    class NodeTriangulation : IDisposable, IEnumerable<EditableIndexTriangle>
    {
        private readonly LinkedListNode<EditableIndexTriangle> FirstTriangle;
        private readonly LinkedListNode<EditableIndexTriangle> LastTriangle;

        public bool IsDisposed { get; private set; }

        public NodeTriangulation(LinkedListNode<EditableIndexTriangle> firstTriangle, LinkedListNode<EditableIndexTriangle> lastTriangle)
        {
            if (firstTriangle.List != lastTriangle.List)
            {
                throw new ArgumentException();
            }

            FirstTriangle = firstTriangle ?? throw new ArgumentNullException(nameof(firstTriangle));
            LastTriangle = lastTriangle ?? throw new ArgumentNullException(nameof(lastTriangle));

            IsDisposed = false;
        }

        public override string ToString()
        {
            return $"{{node triangulation: {FirstTriangle?.Value} - {LastTriangle?.Value}}}";
        }

        public void Dispose()
        {
            if (IsDisposed)
            {
                throw new InvalidOperationException();
            }
            IsDisposed = true;

            void remove(LinkedListNode<EditableIndexTriangle> node) => node.List.Remove(node);

            if (FirstTriangle == LastTriangle)
            {
                remove(FirstTriangle);
                return;
            }

            //remove all triangles from first until last
            var current = FirstTriangle;
            while (current != LastTriangle)
            {
                var next = current.Next;
                remove(current);
                current = next;
            }

            remove(LastTriangle);

        }

        public IEnumerator<EditableIndexTriangle> GetEnumerator()
        {
            if (IsDisposed)
            {
                throw new InvalidOperationException();
            }

            var current = FirstTriangle;

            while (current != LastTriangle)
            {
                yield return current.Value;

                current = current.Next;
            }

            yield return current.Value;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
