using GeometricAlgorithms.Domain.Tasks;
using GeometricAlgorithms.Viewer.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Viewer.Model
{
    public class ModelData
    {
        public ViewerConfiguration ViewerConfiguration { get; set; }

        public Workspace Workspace { get; set; }

        public ModelData(IDrawableFactoryProvider drawableFactoryProvider, IFuncExecutor funcExecutor)
        {
            ViewerConfiguration = new ViewerConfiguration();
            Workspace = new Workspace(drawableFactoryProvider);
        }
    }
}
