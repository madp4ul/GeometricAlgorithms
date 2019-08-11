using GeometricAlgorithms.Domain.Tasks;
using GeometricAlgorithms.Viewer.Utilities.BackgroundWorkerFunctions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Viewer.Utilities
{
    class BackgroundWorkerFuncExecutor : IFuncExecutor, IDisposable
    {
        private readonly BackgroundWorker Worker;
        private readonly ConcurrentQueue<Action> FunctionQueue;
        private readonly BackgroundWorkerProgressUpdater WorkerStatusUpdater;
        private readonly IProgressUpdater UserProgressUpdater;

        private bool IsDisposed;


        public BackgroundWorkerFuncExecutor(IProgressUpdater progressUpdater)
        {
            UserProgressUpdater = progressUpdater;

            Worker = new BackgroundWorker();
            Worker.DoWork += ExecuteQueue;

            Worker.WorkerReportsProgress = true;
            Worker.ProgressChanged += Worker_ProgressChanged;
            Worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

            WorkerStatusUpdater = new RateLimitedBackgroundWorkerProgressUpdater(Worker);

            FunctionQueue = new ConcurrentQueue<Action>();
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Restart if enqueue was missed
            if (!FunctionQueue.IsEmpty && !Worker.IsBusy)
            {
                Worker.RunWorkerAsync();
            }
        }

        ~BackgroundWorkerFuncExecutor()
        {
            Dispose();
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState is IWorkerProgressUpdate result)
            {
                result.ExecuteUpdate();
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private void ExecuteQueue(object sender, DoWorkEventArgs e)
        {
            while (!FunctionQueue.IsEmpty)
            {
                //Try to get actions until it works
                if (FunctionQueue.TryDequeue(out Action action))
                {
                    action();
                }
            }
        }

        public IFuncExecution<T> Execute<T>(Func<IProgressUpdater, T> function)
        {
            BackgroundWorkerExecution<T> execution = new BackgroundWorkerExecution<T>(function, UserProgressUpdater);

            void backgroundWork()
            {
                execution.Function(WorkerStatusUpdater);
            }

            FunctionQueue.Enqueue(backgroundWork);

            if (!Worker.IsBusy)
            {
                Worker.RunWorkerAsync();
            }

            return execution;
        }

        public void Dispose()
        {
            if (!IsDisposed)
            {
                Worker.Dispose();
                IsDisposed = true;
            }
        }
    }
}
