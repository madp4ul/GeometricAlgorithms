using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using GeometricAlgorithms.VertexTypes;

namespace GeometricAlgorithms
{
    public interface IModel<TVertex> : I3DQueryable<TVertex>
        where TVertex : Vertex
    {
        TVertex[] Vertices { get; }
        int[] Indices { get; }
    }

    //Default
    public interface IModel : IModel<Vertex>
    {
    }
}
