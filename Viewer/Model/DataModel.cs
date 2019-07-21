using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Viewer.Model
{
    public class DataModel
    {
        public ViewerConfiguration ViewerConfiguration { get; set; }

        public Workspace Workspace { get; set; }

        public DataModel()
        {
            ViewerConfiguration = new ViewerConfiguration();
            Workspace = new Workspace();
        }
    }
}
