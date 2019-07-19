using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.FileProcessing
{
    public class FileReader
    {
        public string[] ReadFile(string path)
        {
            return File.ReadAllLines(path);
        }
    }
}
