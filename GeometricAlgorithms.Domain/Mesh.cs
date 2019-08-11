using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Domain
{
    public class Mesh
    {
        public int VertexCount { get; private set; }

        public ReadOnlyCollection<Vector3> Positions { get; private set; }

        /// <summary>
        /// Normals read from file
        /// </summary>
        public ReadOnlyCollection<Vector3> UnitNormals { get; set; }

        /// <summary>
        /// Faces of mesh
        /// </summary>
        public ReadOnlyCollection<Triangle> Faces { get; private set; }

        public Mesh(Vector3[] positions, Triangle[] fileFaces, Vector3[] fileNormals = null)
        {
            Positions = Array.AsReadOnly(positions) ?? throw new ArgumentNullException(nameof(positions));
            Faces = fileFaces != null ? Array.AsReadOnly(fileFaces) : null;
            UnitNormals = fileNormals != null ? Array.AsReadOnly(fileNormals) : null;

            VertexCount = positions.Length;
        }

        public IEnumerable<Vector3> GetFacePositions(int index)
        {
            return Faces[index].Select(vIndex => Positions[vIndex]);
        }

        public Mesh Copy(Vector3[] replacePositions = null, Vector3[] replaceNormals = null)
        {
            return new Mesh(
                replacePositions ?? Positions.ToArray(),
                Faces?.ToArray(),
                replaceNormals ?? UnitNormals?.ToArray());
        }

        public static Mesh CreateEmpty()
        {
            return new Mesh(new Vector3[0], new Triangle[0]);
        }
    }
}
