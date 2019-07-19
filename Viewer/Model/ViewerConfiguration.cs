using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Viewer.Model
{
    public class ViewerConfiguration
    {
        public float MouseSensitivity { get; set; }

        public ViewerConfiguration()
        {
            MouseSensitivity = 0.1f;
        }
    }
}
