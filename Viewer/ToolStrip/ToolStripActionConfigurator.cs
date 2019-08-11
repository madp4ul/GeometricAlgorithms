using GeometricAlgorithms.Viewer.Model;
using GeometricAlgorithms.Viewer.ToolStrip.Configurators;
using GeometricAlgorithms.Viewer.ToolStrip.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeometricAlgorithms.Viewer.ToolStrip
{
    class ToolStripActionConfigurator
    {
        public MainWindow MainWindow { get; set; }
        public MenuStrip MenuStrip { get; set; }

        public ModelData Model { get; set; }

        public ToolStripActionConfigurator(MainWindow mainWindow, MenuStrip menuStrip, ModelData model)
        {
            MainWindow = mainWindow ?? throw new ArgumentNullException(nameof(mainWindow));
            MenuStrip = menuStrip ?? throw new ArgumentNullException(nameof(menuStrip));
            Model = model ?? throw new ArgumentNullException(nameof(model));
        }

        public void Configure()
        {
            new FileMenuConfigurator(Model).Configure(MenuStrip);
            new ViewerMenuConfigurator(Model).Configure(MenuStrip);
            new KdTreeMenuConfigurator(Model, MainWindow).Configure(MenuStrip);
            new ApproximationConfigurator(Model, MainWindow).Configure(MenuStrip);
        }
    }
}
