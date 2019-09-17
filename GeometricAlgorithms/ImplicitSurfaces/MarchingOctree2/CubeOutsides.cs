using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree2
{
    /// <summary>
    /// Cleaner access to sides of a cube by requiring orientation
    /// </summary>
    class CubeOutsides
    {
        private readonly Side[] Sides = new Side[6];

        private readonly Lazy<CubeInsides> ChildSides;

        private CubeOutsides()
        {
            ChildSides = new Lazy<CubeInsides>(() => new CubeInsides(this));
        }

        public Side this[SideOrientation orientation]
        {
            get { return Sides[orientation.GetArrayIndex()]; }
            set { Sides[orientation.GetArrayIndex()] = value; }
        }

        public CubeOutsides CreateChild(OctreeOffset childOffset)
        {
            var childSides = ChildSides.Value;

            //TODO get the right child sides and create filled container for child
            //use inside container because it stores the child sides for later creations of siblings

            throw new NotImplementedException();
        }

        public static CubeOutsides ForRoot()
        {
            //TODO create container and fill with newly creates sides from all new data

            throw new NotImplementedException();
        }
    }
}
