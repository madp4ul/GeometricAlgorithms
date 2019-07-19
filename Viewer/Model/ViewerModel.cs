using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Viewer.Model
{
    public class ViewerModel
    {
        public ViewerConfiguration ViewerConfiguration { get; set; }

        public ViewerModel()
        {
            ViewerConfiguration = new ViewerConfiguration();
        }
    }
}
