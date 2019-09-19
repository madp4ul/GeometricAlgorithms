using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree2
{
    class EdgeTreeNode
    {
        public readonly CubeOutsides Sides;

        public EdgeTreeNode[,,] Children { get; private set; }
        public bool HasChildren => Children != null;

        private EdgeTreeNode()
        {
            //actual sides will be provides by parent
            Sides = CubeOutsides.ForRoot();
        }

        private EdgeTreeNode(CubeOutsides outsides)
        {
            //actual sides will be provides by parent
            Sides = outsides;
        }

        public void CreateChildren()
        {
            if (HasChildren)
            {
                throw new InvalidOperationException();
            }

            var children = new EdgeTreeNode[2, 2, 2];

            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < 2; y++)
                {
                    for (int z = 0; z < 2; z++)
                    {
                        OctreeOffset offset = new OctreeOffset(x, y, z);

                        var childOutsides = Sides.CreateChild(offset);

                        children[x, y, z] = new EdgeTreeNode(childOutsides);
                    }
                }
            }

            Children = children;
        }

        public static EdgeTreeNode CreateRoot()
        {
            return new EdgeTreeNode();
        }
    }
}
