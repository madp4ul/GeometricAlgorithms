using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Domain
{
    public class Mesh<TVertex>
    {
        public TVertex[] Vertices { get; private set; }

        public IFace[] Faces { get; private set; }

        public Mesh(TVertex[] vertices, IFace[] faces)
        {
            Vertices = vertices ?? throw new ArgumentNullException(nameof(vertices));
            Faces = faces ?? throw new ArgumentNullException(nameof(faces));
        }

        public IEnumerable<TVertex> GetFaceVertices(int index)
        {
            return Faces[index].Select(vIndex => Vertices[vIndex]);
        }

        public Mesh<TVertex> Copy()
        {
            return new Mesh<TVertex>(Vertices.ToArray(), Faces.ToArray());
        }

        public static Mesh<TVertex> CreateEmpty()
        {
            return new Mesh<TVertex>(new TVertex[0], new IFace[0]);
        }
    }
}
