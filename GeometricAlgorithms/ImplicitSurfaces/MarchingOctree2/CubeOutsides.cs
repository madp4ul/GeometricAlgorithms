using GeometricAlgorithms.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree2
{
    /// <summary>
    /// Cleaner access to sides of a cube by requiring orientation
    /// </summary>
    class CubeOutsides : IEnumerable<Side>
    {
        private readonly ImplicitSurfaceProvider ImplicitSurface;

        private readonly Side[] Sides = new Side[6];

        public CubeOutsideChildren Children { get; private set; }
        public bool HasChildren => Children != null;

        private CubeOutsides(ImplicitSurfaceProvider implicitSurface)
        {
            ImplicitSurface = implicitSurface;
        }

        public Side this[SideOrientation orientation]
        {
            get { return Sides[orientation.GetArrayIndex()]; }
            set { Sides[orientation.GetArrayIndex()] = value; }
        }

        public void CreateChildren()
        {
            var insides = new CubeInsides(ImplicitSurface, this);

            var children = new CubeOutsides[2, 2, 2];

            //get the right child sides and create filled container for child
            //use inside container because it stores the child sides for later creations of siblings
            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < 2; y++)
                {
                    for (int z = 0; z < 2; z++)
                    {
                        var parentOffset = new OctreeOffset(x, y, z);

                        var child = new CubeOutsides(ImplicitSurface);

                        for (int i = 0; i < child.Sides.Length; i++)
                        {
                            SideOrientation orientation = new SideOrientation(SideOrientation.GetSideIndex(i));

                            if (orientation.IsInside(parentOffset))
                            {
                                child[orientation] = insides[orientation, parentOffset];
                            }
                            else
                            {
                                SideOffset childSideOffset = parentOffset.ExcludeDimension(orientation.GetAxis());
                                child[orientation] = Sides[orientation.GetArrayIndex()].Children[
                                    childSideOffset.MinimumDimensionValue,
                                    childSideOffset.MaximumDimensionValue];
                            }
                        }
                    }
                }
            }

            Children = new CubeOutsideChildren(children);
        }

        public static CubeOutsides ForRoot(ImplicitSurfaceProvider implicitSurface, BoundingBox boundingBox)
        {
            FunctionValue[] cornerValues = new FunctionValue[8];

            for (int i = 0; i < cornerValues.Length; i++)
            {
                FunctionValueOrientation orientation = new FunctionValueOrientation(FunctionValueOrientation.GetFunctionValueIndex(i));

                Vector3 corner = orientation.GetCorner(boundingBox);

                cornerValues[i] = implicitSurface.CreateFunctionValue(corner);
            }

            Edge[] edges = new Edge[12];

            for (int i = 0; i < edges.Length; i++)
            {
                EdgeOrientation orientation = new EdgeOrientation(EdgeOrientation.GetEdgeIndex(i));

                FunctionValue minValue = cornerValues[orientation.GetValueOrientation(0).GetArrayIndex()];
                FunctionValue maxValue = cornerValues[orientation.GetValueOrientation(1).GetArrayIndex()];

                edges[i] = new Edge(implicitSurface, orientation.GetAxis(), minValue, maxValue);
            }

            CubeOutsides rootOusides = new CubeOutsides(implicitSurface);

            for (int i = 0; i < rootOusides.Sides.Length; i++)
            {
                SideOrientation orientation = new SideOrientation(SideOrientation.GetSideIndex(i));

                SideOutsideEdges sideEdges = new SideOutsideEdges(orientation);

                for (int a = 0; a < 2; a++)
                {
                    for (int b = 0; b < 2; b++)
                    {
                        Edge sideEdge = edges[orientation.GetEdgeOrientation(a, b).GetArrayIndex()];

                        sideEdges[a, b] = sideEdge;
                    }
                }

                rootOusides[orientation] = new Side(implicitSurface, sideEdges);
            }

            return rootOusides;
        }

        public IEnumerator<Side> GetEnumerator()
        {
            for (int i = 0; i < Sides.Length; i++)
            {
                yield return Sides[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
