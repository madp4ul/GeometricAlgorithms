using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometricAlgorithms.Domain;

namespace GeometricAlgorithms
{
    public interface IModel<TVertex> : I3DQueryable<TVertex>
        where TVertex : IVertex
    {
        TVertex[] Vertices { get; }
        int[] Indices { get; }
    }

    //Default
    public interface IModel : IModel<IVertex>
    {
    }
}
