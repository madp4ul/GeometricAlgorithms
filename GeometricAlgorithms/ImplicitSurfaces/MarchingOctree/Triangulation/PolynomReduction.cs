﻿using GeometricAlgorithms.Domain;
using GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.Approximation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.Triangulation
{
    class PolynomReduction
    {
        public TriangleLineSegmentNode Node { get; private set; }
        private bool SelectNext;

        public bool IsValid => Node != null && Node.Previous != null && Node.Next != null;

        public PolynomReduction(TriangleLineSegmentNode node)
        {
            Node = node ?? throw new ArgumentNullException(nameof(node));
            SelectNext = true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="triangle">triangle that covers the space removed by this operation</param>
        /// <returns>if further movements can be made</returns>
        public bool MoveToNextPoint(out EditableIndexTriangle triangle)
        {
            triangle = new EditableIndexTriangle(Node.Intersection, Node.Previous.Intersection, Node.Next.Intersection);

            if (Node.Previous.Previous == Node.Next)
            {
                return false;
            }

            TriangleLineSegmentNode.Connect(Node.Previous, Node.Next);

            var oldNode = Node;
            Node = SelectNext ? Node.Next : Node.Previous;
            SelectNext = !SelectNext;

            oldNode.Detach();

            return true;
        }

        public override string ToString()
        {
            return $"{{reduction: {Node.ToString()}}}";
        }
    }
}
