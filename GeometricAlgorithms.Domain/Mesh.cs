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
        public ReadOnlyCollection<Vector3> FileUnitNormals { get; set; }

        /// <summary>
        /// Normals Approximated from faces of mesh
        /// </summary>
        public Vector3[] FaceApproximatedUnitNormals { get; set; }

        /// <summary>
        /// Normals approximated from pure point cloud
        /// </summary>
        public Vector3[] PointApproximatedUnitNormals { get; set; }

        /// <summary>
        /// Faces of mesh
        /// </summary>
        public ReadOnlyCollection<Triangle> FileFaces { get; private set; }

        /// <summary>
        /// Faces approximated by algorithm
        /// </summary>
        public Triangle[] ApproximatedFaces { get; set; }

        public Mesh(Vector3[] positions, Triangle[] fileFaces, Vector3[] fileNormals = null)
        {
            Positions = Array.AsReadOnly(positions) ?? throw new ArgumentNullException(nameof(positions));
            FileFaces = fileFaces != null ? Array.AsReadOnly(fileFaces) : null;
            FileUnitNormals = fileNormals != null ? Array.AsReadOnly(fileNormals) : null;

            VertexCount = positions.Length;
        }

        public IEnumerable<Vector3> GetFacePositions(int index)
        {
            return FileFaces[index].Select(vIndex => Positions[vIndex]);
        }

        public Mesh Copy(Vector3[] replacePositions = null)
        {
            return new Mesh(replacePositions ?? Positions.ToArray(), FileFaces?.ToArray(), FileUnitNormals?.ToArray())
            {
                FaceApproximatedUnitNormals = this.FaceApproximatedUnitNormals?.ToArray(),
                PointApproximatedUnitNormals = this.PointApproximatedUnitNormals?.ToArray(),
            };
        }

        public static Mesh CreateEmpty()
        {
            return new Mesh(new Vector3[0], new Triangle[0]);
        }
    }
}
