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
        IEnumerable<TVertex> Vertices { get; }
        IEnumerable<int> Indices { get; }
    }

    //Default
    public interface IModel : IModel<Vertex>
    {
    }
}
