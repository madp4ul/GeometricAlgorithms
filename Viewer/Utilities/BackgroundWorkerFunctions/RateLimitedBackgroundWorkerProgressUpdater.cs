using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Viewer.Utilities.BackgroundWorkerFunctions
{
    /// <summary>
    /// Add Rate Limiting to BackgroundWorkerProgressUpdater to not drown main thread in updates.
    /// Only updates with changes to percentDone will be reported
    /// </summary>
    class RateLimitedBackgroundWorkerProgressUpdater : BackgroundWorkerProgressUpdater
    {
        private int LastPercent;

        public RateLimitedBackgroundWorkerProgressUpdater(BackgroundWorker worker) : base(worker)
        {
        }

        public override void UpdateStatus(int percentDone, string message = null)
        {
            //only update inner if progress has visibly changed
            if (percentDone == 100 || percentDone != LastPercent)
            {
                LastPercent = percentDone;
                base.UpdateStatus(percentDone, message);
            }
        }
    }
}
