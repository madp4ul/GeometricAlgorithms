using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree2
{
    class Side
    {
        private readonly ImplicitSurfaceProvider ImplicitSurface;

        public readonly SideDimensions Dimensions;

        public readonly SideOutsideEdges Edges;

        public SideChildren Children { get; private set; }
        public bool HasChildren => Children != null;

        public Side(ImplicitSurfaceProvider implicitSurface, SideOutsideEdges edges)
        {
            if (edges.IsEdgeMissing)
            {
                throw new ArgumentException("Side can only be constructed from 4 edges.");
            }

            ImplicitSurface = implicitSurface;
            Edges = edges;
            Dimensions = edges.Dimensions;
        }

        public void CreateChildren()
        {
            if (HasChildren)
            {
                throw new InvalidOperationException("Cant compute children twice.");
            }

            var insideEdges = new SideInsideEdges(ImplicitSurface, Edges);

            SideOutsideEdges child00Edges = new SideOutsideEdges(Dimensions.DirectionAxisFromCubeCenter);
            child00Edges[0, 0] = Edges[0, 0].Children[0];
            child00Edges[0, 1] = insideEdges[1, 0];
            child00Edges[1, 0] = Edges[1, 0].Children[0];
            child00Edges[1, 1] = insideEdges[0, 0];
            Side child00 = new Side(ImplicitSurface, child00Edges);

            SideOutsideEdges child01Edges = new SideOutsideEdges(Dimensions.DirectionAxisFromCubeCenter);
            child00Edges[0, 0] = Edges[0, 0].Children[1];
            child00Edges[0, 1] = insideEdges[1, 1];
            child00Edges[1, 0] = insideEdges[0, 0]; 
            child00Edges[1, 1] = Edges[1, 1].Children[0];
            Side child01 = new Side(ImplicitSurface, child01Edges);
            //TODO fix the numbers

            SideOutsideEdges child10Edges = new SideOutsideEdges(Dimensions.DirectionAxisFromCubeCenter);
            child00Edges[0, 0] = insideEdges[1, 0]; 
            child00Edges[0, 1] = Edges[0, 1].Children[0];
            child00Edges[1, 0] = Edges[1, 0].Children[1];
            child00Edges[1, 1] = insideEdges[0, 1];
            Side child10 = new Side(ImplicitSurface, child10Edges);

            SideOutsideEdges child11Edges = new SideOutsideEdges(Dimensions.DirectionAxisFromCubeCenter);
            child00Edges[0, 0] = insideEdges[1, 1];
            child00Edges[0, 1] = Edges[0, 1].Children[1];
            child00Edges[1, 0] = insideEdges[0, 1]; 
            child00Edges[1, 1] = Edges[1, 1].Children[1];
            Side child11 = new Side(ImplicitSurface, child11Edges);

            Children = new SideChildren(child00, child01, child10, child11);
        }
    }
}
