using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Domain.VertexTypes
{
    public struct GenericVertex : IVertex
    {
        public Vector3 Position { get; set; }

        public object Data { get; set; }

        public GenericVertex(Vector3 position, object data = null)
        {
            Position = position;
            Data = data;
        }

        public T GetData<T>()
        {
            return (T)Data;
        }
    }
}
