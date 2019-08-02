using GeometricAlgorithms.Domain.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Viewer.Utilities.BackgroundWorkerFunctions
{
    class RateLimitProgressUpdate : IProgressUpdater
    {
        private readonly IProgressUpdater Updater;
        private int LastPercent;

        public RateLimitProgressUpdate(IProgressUpdater updater)
        {
            Updater = updater ?? throw new ArgumentNullException(nameof(updater));
        }

        public void UpdateStatus(int percentDone, string message = null)
        {
            //only update inner if progress has visibly changed
            if (percentDone == 100 || percentDone != LastPercent)
            {
                LastPercent = percentDone;
                Updater.UpdateStatus(percentDone, message);
            }

        }
    }
}
