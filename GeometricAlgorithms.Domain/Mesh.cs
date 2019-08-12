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

        public Mesh(Vector3[] positions, Triangle[] faces = null, Vector3[] normals = null)
        {
            Positions = Array.AsReadOnly(positions) ?? throw new ArgumentNullException(nameof(positions));
            Faces = faces != null ? Array.AsReadOnly(faces) : null;
            UnitNormals = normals != null ? Array.AsReadOnly(normals) : null;

            VertexCount = positions.Length;
        }

        public bool HasFaces => Faces != null;
        public bool HasNormals => UnitNormals != null;

        public Mesh Copy(
            ICollection<Vector3> replacePositions = null,
            ICollection<Vector3> replaceNormals = null)
        {
            if (replacePositions != null && replacePositions.Count != Positions.Count
                || replaceNormals != null && replaceNormals.Count != Positions.Count)
            {
                throw new ArgumentException(
                    "Can not replace positions or normals with different amount of position or normals");
            }

            return new Mesh(
                (replacePositions ?? Positions).ToArray(),
                Faces?.ToArray(),
                (replaceNormals ?? UnitNormals)?.ToArray());
        }

        public static Mesh CreateEmpty()
        {
            return new Mesh(new Vector3[0]);
        }
    }
}
