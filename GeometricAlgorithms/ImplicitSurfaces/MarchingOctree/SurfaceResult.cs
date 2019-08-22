using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree
{
    class SurfaceResult
    {
        private readonly List<Vector3> Positions;
        private readonly List<Triangle> Faces;

        public SurfaceResult()
        {
            Positions = new List<Vector3>();
            Faces = new List<Triangle>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <returns>index if added position in result</returns>
        public int AddPosition(Vector3 position)
        {
            Positions.Add(position);
            return Positions.Count - 1;
        }

        public void AddFaces(IEnumerable<Triangle> triangles)
        {
            Faces.AddRange(triangles);
        }

        public Vector3[] GetPositions()
        {
            return Positions.ToArray();
        }

        public Triangle[] GetFaces()
        {
            return Faces.ToArray();
        }
    }
}
