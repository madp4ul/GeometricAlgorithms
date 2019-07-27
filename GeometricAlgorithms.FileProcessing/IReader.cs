using GeometricAlgorithms.Domain.VertexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.FileProcessing
{
    public interface IReader
    {
        GenericVertex[] ReadPoints(string filePath);
    }
}
