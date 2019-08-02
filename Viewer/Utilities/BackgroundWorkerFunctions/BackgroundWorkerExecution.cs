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
        public readonly Action<BackgroundWorkerProgressUpdater> Function;

        public bool IsStarted { get; private set; }
        public bool IsDone { get; private set; }
        public T Result { get; private set; }

        public event Action<T> FuncDone;

        public BackgroundWorkerExecution(Func<IProgressUpdater, T> function, IProgressUpdater statusUpdater)
        {
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

        public void GetResult(Action<T> action)
        {
            //Because in both cases action will be executed be the main thread,
            //We can be sure that FuncDone will not be called after IsDone evaluated to false
            //but before the action was added.
            //For that reason nothing complicated is needed here
            if (IsDone)
            {
                action(Result);
            }
            else
            {
                FuncDone += action;
            }
        }
    }
}
