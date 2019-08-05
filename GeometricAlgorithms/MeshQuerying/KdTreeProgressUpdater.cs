using GeometricAlgorithms.Domain.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.MeshQuerying
{
    class KdTreeProgressUpdater
    {
        private readonly IProgressUpdater Updater;

        private readonly string OperationDescription;
        private readonly int TotalOperations;
        private int OperationsCompleted;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="updater"></param>
        /// <param name="totalOperations">Predicted total amount of operations, 
        /// if unsure guess too high instead of too low</param>
        /// <param name="operationDescription"></param>
        public KdTreeProgressUpdater(IProgressUpdater updater, int totalOperations, string operationDescription)
        {
            Updater = updater;
            OperationDescription = operationDescription;
            TotalOperations = totalOperations;
            OperationsCompleted = 0;
        }

        /// <summary>
        /// Add progress towards reaching goal
        /// </summary>
        /// <param name="operationCount"></param>
        public void UpdateAddOperation(int operationCount = 1)
        {
            if (Updater != null)
            {
                OperationsCompleted += operationCount;

                //because TotalOperations isnt necessarily accurate we dont rely on it to be the max value
                int maxOperationCount = Math.Max(OperationsCompleted, TotalOperations);

                int percentDone = (int)(100 * (OperationsCompleted / (float)maxOperationCount));

                string message = $"{OperationsCompleted}/{maxOperationCount} {OperationDescription}";

                Updater.UpdateStatus(percentDone, message);
            }
        }

        public void IsCompleted()
        {
            Updater?.UpdateStatus(100, $"{OperationDescription} complete");
        }
    }
}
