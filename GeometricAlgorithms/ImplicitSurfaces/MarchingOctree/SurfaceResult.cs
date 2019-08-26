using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree
{
    public class SurfaceResult
    {
        private readonly List<Vector3> Positions = new List<Vector3>();
        private readonly List<Triangle> Faces = new List<Triangle>();

        private readonly List<FunctionValue> InnerValues = new List<FunctionValue>();
        private readonly List<FunctionValue> OuterValues = new List<FunctionValue>();

        public SurfaceResult()
        {
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

        public void AddFace(Triangle triangle)
        {
            Faces.Add(triangle);
        }

        public void AddFaces(IEnumerable<Triangle> triangles)
        {
            Faces.AddRange(triangles);
        }

        public void AddFunctionValue(FunctionValue value)
        {
            var list = value.IsInside ? InnerValues : OuterValues;
            list.Add(value);
        }

        public Vector3[] GetPositions()
        {
            return Positions.ToArray();
        }

        public Triangle[] GetFaces()
        {
            return Faces.ToArray();
        }

        public FunctionValue[] GetInnerValues()
        {
            return InnerValues.ToArray();
        }

        public FunctionValue[] GetouterValues()
        {
            return OuterValues.ToArray();
        }
    }
}
