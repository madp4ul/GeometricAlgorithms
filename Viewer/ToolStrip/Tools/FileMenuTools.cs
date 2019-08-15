using GeometricAlgorithms.FileProcessing;
using GeometricAlgorithms.Viewer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeometricAlgorithms.Viewer.ToolStrip.Tools
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

            using (var openFileDialog = new OpenFileDialog
            {
                //InitialDirectory = @"C:\",
                Filter = "Off-Dateien|*.off",
                RestoreDirectory = true,
            })
            {
                //TODO try to find bug by checking permission
                var permission = new FileIOPermission(FileIOPermissionAccess.Read, openFileDialog.InitialDirectory);
                permission.Demand();

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var reader = new ModelReader();
                    var mesh = reader.ReadPoints(openFileDialog.FileName);

                    ViewerModel.Workspace.Update(mesh);
                }
            }
        }

        public void SaveToFile()
        {
            //TODO
        }
    }
}
