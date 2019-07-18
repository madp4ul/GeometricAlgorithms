using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Viewer.ConfigurationModel
{
    public class Configuration
    {
        public ViewerConfiguration ViewerConfiguration { get; set; }

        public Configuration()
        {
            ViewerConfiguration = new ViewerConfiguration();
        }
    }
}
