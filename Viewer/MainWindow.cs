using GeometricAlgorithms.Domain;
using GeometricAlgorithms.FileProcessing;
using GeometricAlgorithms.MonoGame.Forms;
using GeometricAlgorithms.MonoGame.Forms.Cameras;
using GeometricAlgorithms.Viewer.Model;
using GeometricAlgorithms.Viewer.ToolStrip;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeometricAlgorithms.Viewer
{
    public partial class MainWindow : Form
    {
        private readonly ModelData Model;

        public MainWindow()
        {
            InitializeComponent();

            Model = new ModelData(drawableFactoryProvider: viewer);

            viewer.Configuration = Model.ViewerConfiguration;
            viewer.Workspace = Model.Workspace;

            new ToolStripActionConfigurator(this, menuStrip, viewer, Model).Configure();
        }
    }
}
