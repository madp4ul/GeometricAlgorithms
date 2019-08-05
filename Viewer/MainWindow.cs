using GeometricAlgorithms.Domain;
using GeometricAlgorithms.FileProcessing;
using GeometricAlgorithms.MonoGame.Forms;
using GeometricAlgorithms.MonoGame.Forms.Cameras;
using GeometricAlgorithms.Viewer.Model;
using GeometricAlgorithms.Viewer.ToolStrip;
using GeometricAlgorithms.Viewer.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeometricAlgorithms.Viewer
{
    public partial class MainWindow : Form
    {
        private readonly ModelData Model;
        private BackgroundWorkerFuncExecutor FuncExecutor { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            FuncExecutor = new BackgroundWorkerFuncExecutor(
                 new FormProgressUpdater(backgroundWorkerStatusLabel, backgroundWorkerProgressBar));

            Model = new ModelData(
                drawableFactoryProvider: viewer,
                FuncExecutor);

            viewer.Configuration = Model.ViewerConfiguration;
            viewer.Workspace = Model.Workspace;

            new ToolStripActionConfigurator(this, menuStrip, Model).Configure();
        }
        ~MainWindow()
        {
            FuncExecutor.Dispose();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            Model.Workspace.LoadReferenceFrame();
        }
    }
}

