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
        public int FaceCount { get; private set; }

        public ReadOnlyCollection<Vector3> Positions { get; private set; }

        /// <summary>
        /// Normals read from file
        /// </summary>
        public ReadOnlyCollection<Vector3> FileNormals { get; set; }

        /// <summary>
        /// Normals Approximated from faces of mesh
        /// </summary>
        public Vector3[] FaceApproximatedNormals { get; set; }

        /// <summary>
        /// Normals approximated from pure point cloud
        /// </summary>
        public Vector3[] PointApproximatedNormals { get; set; }

        /// <summary>
        /// Faces of mesh
        /// </summary>
        public ReadOnlyCollection<IFace> Faces { get; private set; }

        public Mesh(Vector3[] positions, IFace[] faces, Vector3[] fileNormals = null)
        {
            Positions = Array.AsReadOnly(positions) ?? throw new ArgumentNullException(nameof(positions));
            Faces = Array.AsReadOnly(faces) ?? throw new ArgumentNullException(nameof(faces));
            FileNormals = fileNormals != null ? Array.AsReadOnly(fileNormals) : null;

            VertexCount = positions.Length;
            FaceCount = faces.Length;
        }

        public IEnumerable<Vector3> GetFacePositions(int index)
        {
            return Faces[index].Select(vIndex => Positions[vIndex]);
        }

        public Mesh Copy(Vector3[] replacePositions = null)
        {
            return new Mesh(replacePositions ?? Positions.ToArray(), Faces.ToArray(), FileNormals?.ToArray())
            {
                FaceApproximatedNormals = this.FaceApproximatedNormals?.ToArray(),
                PointApproximatedNormals = this.PointApproximatedNormals?.ToArray(),
            };
        }

        public static Mesh CreateEmpty()
        {
            return new Mesh(new Vector3[0], new IFace[0]);
        }
    }
}
