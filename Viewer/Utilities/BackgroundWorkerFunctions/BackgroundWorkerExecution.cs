using GeometricAlgorithms.Domain.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Viewer.Utilities.BackgroundWorkerFunctions
{
    class BackgroundWorkerExecution<T> : IFuncExecution<T>
    {
        private readonly BackgroundWorker Worker;
        public readonly Action<WorkerStatusUpdater> Function;

        public bool IsStarted { get; private set; }
        public bool IsDone { get; private set; }
        public T Result { get; private set; }

        public event Action<T> FuncDone;


        public BackgroundWorkerExecution(BackgroundWorker worker, Func<IProgressUpdater, T> function, IProgressUpdater statusUpdater)
        {
            Worker = worker ?? throw new ArgumentNullException(nameof(worker));
            Function = (workerStatus) =>
            {
                //Set values on execution environment
                workerStatus.CurrentStatusUpdater = statusUpdater;

                //Execute task
                IsStarted = true;
                T result = function(workerStatus);

                //Store result and inform listeners about it
                Result = result;
                IsDone = true;
                workerStatus.OnFunctionFinished(result, FuncDone);
            };
        }

        public void CallWithResult(Action<T> action)
        {
            T value = default(T);
            bool wasDone = false;

            //Dont allow writing result to field while checking if the value is there.
            lock (Result)
            {
                if (IsDone)
                {
                    value = Result;
                    wasDone = true;
                }
                else
                {
                    //If still waiting for value, add action to listeners.
                    //The blocking of result guarantees that listeners were not executed in the meantime.
                    FuncDone += action;
                }
            }

            //Is the value was already there: execute action immediatly
            if (wasDone)
            {
                action(value);
            }
        }
    }
}
