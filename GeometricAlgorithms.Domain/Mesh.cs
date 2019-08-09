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
        public ReadOnlyCollection<Triangle> FileFaces { get; private set; }

        public Mesh(Vector3[] positions, Triangle[] fileFaces, Vector3[] fileNormals = null)
        {
            Positions = Array.AsReadOnly(positions) ?? throw new ArgumentNullException(nameof(positions));
            FileFaces = fileFaces != null ? Array.AsReadOnly(fileFaces) : null;
            FileNormals = fileNormals != null ? Array.AsReadOnly(fileNormals) : null;

            VertexCount = positions.Length;
        }

        public IEnumerable<Vector3> GetFacePositions(int index)
        {
            return FileFaces[index].Select(vIndex => Positions[vIndex]);
        }

        public Mesh Copy(Vector3[] replacePositions = null)
        {
            return new Mesh(replacePositions ?? Positions.ToArray(), FileFaces?.ToArray(), FileNormals?.ToArray())
            {
                FaceApproximatedNormals = this.FaceApproximatedNormals?.ToArray(),
                PointApproximatedNormals = this.PointApproximatedNormals?.ToArray(),
            };
        }

        public static Mesh CreateEmpty()
        {
            return new Mesh(new Vector3[0], new Triangle[0]);
        }
    }
}
