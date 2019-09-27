using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree
{
    struct EdgeIntersectionCache
    {
        private SurfaceApproximation UsedApproximation;
        private PositionIndex[] IntersectionIndices;
        private EdgeSurfaceIntersections Intersections;

        public void SetIntersectionIndices(SurfaceApproximation approximation, PositionIndex[] writeableIndices)
        {
            SetUsedApproximation(approximation);

            IntersectionIndices = writeableIndices;
        }

        public bool TryGetIntersectionIndices(SurfaceApproximation approximation, out PositionIndex[] intersectionIndices)
        {
            if (approximation == UsedApproximation)
            {
                intersectionIndices = IntersectionIndices;
                return true;
            }
            else
            {
                intersectionIndices = null;
                return false;
            }
        }

        public void SetIntersections(SurfaceApproximation approximation, EdgeSurfaceIntersections surfaceIntersections)
        {
            SetUsedApproximation(approximation);

            Intersections = surfaceIntersections;
        }

        public bool TryGetIntersections(SurfaceApproximation approximation, out EdgeSurfaceIntersections intersections)
        {
            if (approximation == UsedApproximation)
            {
                intersections = Intersections;
                return true;
            }
            else
            {
                intersections = null;
                return false;
            }
        }


        private void SetUsedApproximation(SurfaceApproximation approximation)
        {
            if (UsedApproximation != approximation)
            {
                UsedApproximation = approximation;
                IntersectionIndices = null;
                Intersections = null;
            }
        }
    }
}
