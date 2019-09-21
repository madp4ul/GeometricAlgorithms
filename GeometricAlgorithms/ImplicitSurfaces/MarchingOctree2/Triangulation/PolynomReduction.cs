using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree2.Triangulation
{
    struct PolynomReduction
    {
        public TriangleLineSegmentNode Node { get; private set; }
        private bool SelectNext;

        public PolynomReduction(TriangleLineSegmentNode node)
        {
            Node = node ?? throw new ArgumentNullException(nameof(node));
            SelectNext = true;
        }

        public static PolynomReduction CreateInitial(TriangleLineSegmentNode node)
        {
            return new PolynomReduction(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="triangle">triangle that covers the space removed by this operation</param>
        /// <returns>if further movements can be made</returns>
        public bool MoveToNextPoint(out Triangle triangle)
        {
            triangle = new Triangle(Node.VertexIndex, Node.Previous.VertexIndex, Node.Next.VertexIndex);

            if (Node.Previous.Previous == Node.Next)
            {
                return false;
            }

            TriangleLineSegmentNode.Connect(Node.Previous, Node.Next);
            Node = SelectNext ? Node.Next : Node.Previous;
            SelectNext = !SelectNext;

            return true;
        }
    }
}
