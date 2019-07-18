using GeometricAlgorithms.Domain;
using GeometricAlgorithms.MonoGame.Forms;
using GeometricAlgorithms.MonoGame.Forms.Cameras;
using GeometricAlgorithms.Viewer.ConfigurationModel;
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
        readonly Configuration Configuration;

        public MainWindows()
        {
            InitializeComponent();

            //Controls.Add(new GeometricAlgorithmViewer());
        }
    }
}
