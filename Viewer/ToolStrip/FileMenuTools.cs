using GeometricAlgorithms.FileProcessing;
using GeometricAlgorithms.Viewer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeometricAlgorithms.Viewer.ToolStrip
{
    class FileMenuTools
    {
        public ModelData ViewerModel { get; set; }

        public FileMenuTools(Model.ModelData viewerModel)
        {
            ViewerModel = viewerModel ?? throw new ArgumentNullException(nameof(viewerModel));
        }

        public void OpenFile()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Off-Dateien|*.off"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var reader = new OFFReader();
                var points = reader.ReadPoints(openFileDialog.FileName);

                ViewerModel.Workspace.PointData.Reset(points, ViewerModel.ViewerConfiguration.PointRadius);
            }
        }

        public void SaveToFile()
        {
            //TODO
        }
    }
}
