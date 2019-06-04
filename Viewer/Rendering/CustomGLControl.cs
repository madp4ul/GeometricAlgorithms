using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;

namespace Viewer.Rendering
{
    public partial class CustomGLControl : GLControl
    {
        public CustomGLControl() : base()
        {
            InitializeComponent();
            
        }

        protected override void OnLoad(EventArgs e)
        {
            

            base.OnLoad(e);
        }
    }
}
