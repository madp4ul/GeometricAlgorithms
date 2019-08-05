using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Domain
{
    public class Mesh
    {
        public int VertexCount { get; private set; }

        public Vector3[] Positions { get; private set; }

        /// <summary>
        /// Normals read from file
        /// </summary>
        public Vector3[] FileNormals { get; set; }

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
        public IFace[] Faces { get; private set; }

        public Mesh(Vector3[] positions, IFace[] faces)
        {
            Positions = positions ?? throw new ArgumentNullException(nameof(positions));
            Faces = faces ?? throw new ArgumentNullException(nameof(faces));

            VertexCount = positions.Length;
        }

        public IEnumerable<Vector3> GetFacePositions(int index)
        {
            return Faces[index].Select(vIndex => Positions[vIndex]);
        }

        public Mesh Copy()
        {
            return new Mesh(Positions.ToArray(), Faces.ToArray())
            {
                FileNormals = this.FileNormals?.ToArray(),
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
