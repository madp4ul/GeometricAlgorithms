using GeometricAlgorithms.Viewer.Forms;
using GeometricAlgorithms.Viewer.Model.KdTreeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Viewer.ToolStrip
{
    class KdTreeMenuTools
    {
        public KdTreeData KdTreeData { get; set; }
        public GeometricAlgorithmViewer Viewer { get; set; }

        private KdTreeOptions SettingsWindow = null;

        public KdTreeMenuTools(KdTreeData kdTreeData, GeometricAlgorithmViewer viewer)
        {
            KdTreeData = kdTreeData ?? throw new ArgumentNullException(nameof(kdTreeData));
            Viewer = viewer ?? throw new ArgumentNullException(nameof(viewer));
        }

        public void SetKdTreeVisibility(bool visible)
        {
            KdTreeData.EnableDraw = visible;
        }

        public void OpenKdTreeSettings(System.Windows.Forms.IWin32Window owner)
        {
            if (SettingsWindow == null)
            {
                SettingsWindow = new KdTreeOptions(KdTreeData, Viewer);
                SettingsWindow.FormClosed += (o, e) => SettingsWindow = null;
                SettingsWindow.Show(owner);
            }
            else
            {
                SettingsWindow.Focus();
            }
        }
    }
}
