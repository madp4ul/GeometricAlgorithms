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

        public KdTreeMenuTools(KdTreeData kdTreeData)
        {
            KdTreeData = kdTreeData ?? throw new ArgumentNullException(nameof(kdTreeData));
        }

        public void SetKdTreeVisibility(bool visible)
        {
            KdTreeData.EnableDraw = visible;
        }

        private KdTreeSettings SettingsWindow = null;
        public void OpenKdTreeSettings(System.Windows.Forms.IWin32Window owner)
        {
            if (SettingsWindow == null)
            {
                SettingsWindow = new KdTreeSettings(KdTreeData);
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
