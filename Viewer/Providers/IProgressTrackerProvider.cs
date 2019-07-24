using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Viewer.Providers
{
    interface IProgressTrackerProvider
    {
        void TrackProgress(int percentDone, string statusMessage);
    }
}
