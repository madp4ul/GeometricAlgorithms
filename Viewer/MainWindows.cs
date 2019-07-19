using GeometricAlgorithms.Domain;
using GeometricAlgorithms.FileProcessing;
using GeometricAlgorithms.MonoGame.Forms;
using GeometricAlgorithms.MonoGame.Forms.Cameras;
using GeometricAlgorithms.Viewer.Model;
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
    public partial class MainWindows : Form
    {
        readonly ViewerModel Configuration;

        MonoGame.Forms.Drawables.IDrawable PointCloud;

        public MainWindows()
        {
            InitializeComponent();
            
            Configuration = new ViewerModel();//todo load or somethign

            viewer.Configuration = Configuration.ViewerConfiguration;
        }

        private void ÖffnenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Off-Dateien|*.off";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var reader = new OFFReader();
                var points = reader.ReadPoints(openFileDialog.FileName);

                if (PointCloud != null)
                {
                    viewer.RemoveFromScene(PointCloud);
                }
                PointCloud = viewer.AddToScene(points);
            }


        }
    }
}
