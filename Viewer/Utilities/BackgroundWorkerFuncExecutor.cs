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
    class BackgroundWorkerFuncExecutor : IFuncExecutor
    {
        private readonly BackgroundWorker Worker;
        private readonly ConcurrentQueue<Action> FunctionQueue;
        private readonly WorkerStatusUpdater WorkerStatusUpdater;

        public BackgroundWorkerFuncExecutor()
        {
            Worker = new BackgroundWorker();
            Worker.DoWork += ExecuteQueue;
            Worker.ProgressChanged += Worker_ProgressChanged;

            WorkerStatusUpdater = new WorkerStatusUpdater(Worker);

            FunctionQueue = new ConcurrentQueue<Action>();
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

        public IFuncExecution<T> Execute<T>(Func<IProgressUpdater, T> function, IProgressUpdater statusUpdater)
        {
            BackgroundWorkerExecution<T> execution = new BackgroundWorkerExecution<T>(Worker, function, statusUpdater);

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
    }
}
