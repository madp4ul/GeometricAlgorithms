using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using GeometricAlgorithms.VertexTypes;

namespace GeometricAlgorithms
{
    public interface IModel<out TVertex> where TVertex : Vertex
    {
        TVertex[] Vertices { get; }
        int[] Indices { get; }

        IReadOnlyList<TVertex> FindNearestVertices(Vector3 searchPosition, int pointAmount);
        IReadOnlyList<TVertex> FindInRadius(Vector3 seachCenter, float searchRadius);
    }

    //Default
    public interface IModel : IModel<Vertex>
    {
    }
}
