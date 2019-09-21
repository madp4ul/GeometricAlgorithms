﻿using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree2
{
    class SurfaceApproximation
    {
        private readonly List<Vector3> Positions = new List<Vector3>();
        private readonly List<Triangle> Faces = new List<Triangle>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <returns>index of added position in result</returns>
        public int AddPosition(Vector3 position)
        {
            Positions.Add(position);
            return Positions.Count - 1;
        }

        public void AddFaces(IEnumerable<Triangle> triangle)
        {
            Faces.AddRange(triangle);
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
