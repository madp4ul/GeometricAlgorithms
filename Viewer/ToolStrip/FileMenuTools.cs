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
            try
            {
                using (var openFileDialog =
                new OpenFileDialog
                {
                    Filter = "Off-Dateien|*.off"
                })
                {
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        var reader = new ModelReader();
                        var points = reader.ReadPoints(openFileDialog.FileName);

                        ViewerModel.Workspace.PointData.Reset(points, ViewerModel.ViewerConfiguration.PointRadius);
                    }
                }
            }
            catch (AccessViolationException ex)
            {
                //TODO find error and add proper handling
                System.Diagnostics.Debugger.Break();

                MessageBox.Show("Error: " + ex.Message);

                throw;
            }


        }

        public void SaveToFile()
        {
            //TODO
        }
    }
}
