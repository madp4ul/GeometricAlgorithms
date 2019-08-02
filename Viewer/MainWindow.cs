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

            Model = new ModelData(drawableFactoryProvider: viewer, FuncExecutor);

            viewer.Configuration = Model.ViewerConfiguration;
            viewer.Workspace = Model.Workspace;

            new ToolStripActionConfigurator(this, menuStrip, viewer, Model).Configure();


            var func1 = FuncExecutor.Execute((status) =>
             {
                 status.UpdateStatus(0, "Start");

                 Thread.Sleep(1000);
                 status.UpdateStatus(30, "Sl11111111111111111eep 1");

                 Thread.Sleep(1000);
                 status.UpdateStatus(60, "Sleep 2");

                 Thread.Sleep(1000);
                 status.UpdateStatus(100, "33333333333333333333333333333 3");

                 return 1;
             });

            func1.GetResult(r => Console.WriteLine($"Result {r}"));

            var func2 = FuncExecutor.Execute((status) =>
            {
                for (int i = 0; i <= 1000; i++)
                {
                    if (i % 10 == 0)
                    {
                        status.UpdateStatus(i / 10, $"{i}/1000 geschafft");
                    }
                    Thread.Sleep(1);
                }

                return 2;
            });

            func2.GetResult(r => Console.WriteLine($"Result 2 {r}"));

        }
        ~MainWindow()
        {
            FuncExecutor.Dispose();
        }
    }
}

