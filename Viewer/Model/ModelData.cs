using GeometricAlgorithms.BusinessLogic;
using GeometricAlgorithms.BusinessLogic.Model;
using GeometricAlgorithms.Domain.Drawables;
using GeometricAlgorithms.Domain.Tasks;
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

        public ModelData(
            IDrawableFactoryProvider drawableFactoryProvider,
            IFuncExecutor funcExecutor,
            IRefreshableView refreshableView)
        {
            ViewerConfiguration = new ViewerConfiguration();
            Workspace = new Workspace(drawableFactoryProvider, funcExecutor, refreshableView);
        }
    }
}
