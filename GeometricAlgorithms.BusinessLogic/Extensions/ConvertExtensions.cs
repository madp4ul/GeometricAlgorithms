using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.BusinessLogic.Extensions
{
    public static class ConvertExtensions
    {
        public static Vector3 ToVector3(this System.Drawing.Color c)
        {
            return new Vector3(c.R / 255f, c.G / 255f, c.B / 255f);
        }
    }
}
