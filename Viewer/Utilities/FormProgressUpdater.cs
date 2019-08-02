using GeometricAlgorithms.Domain.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeometricAlgorithms.Viewer.Utilities
{
    class FormProgressUpdater : IProgressUpdater
    {
        public ToolStripStatusLabel ProgressMessageLabel { get; set; }
        public ToolStripProgressBar ProgressBar { get; set; }

        private const int MaximumProgress = 10000;

        public FormProgressUpdater(ToolStripStatusLabel progressMessageLabel, ToolStripProgressBar progressBar)
        {
            ProgressMessageLabel = progressMessageLabel ?? throw new ArgumentNullException(nameof(progressMessageLabel));
            ProgressBar = progressBar ?? throw new ArgumentNullException(nameof(progressBar));

            ProgressBar.Maximum = MaximumProgress;
        }

        public void UpdateStatus(int percentDone, string message = null)
        {
            //Scaling from 100 to 10000
            int scaledProgress = percentDone * 100;

            ProgressBar.Value = scaledProgress;

            //Due to bug with with windows animation we have to go backwards one step at the end
            //Otherwise the progress bar movement would just stop without reaching the end
            //https://stackoverflow.com/questions/23476932/progress-bar-does-not-reach-100
            if (ProgressBar.Maximum == scaledProgress)
            {
                ProgressBar.Value = scaledProgress - 1;
            }

            ProgressMessageLabel.Text = message;
        }
    }
}
