using System;
using System.Threading;
using System.Threading.Tasks;

namespace Worker.Api.Worker
{
    public class WorkerService : HostedService
    {
        private readonly WorkerQueue workerQueue;
        private readonly WorkerListener listener;

        public WorkerService(WorkerQueue workerQueue, WorkerListener listener)
        {
            this.listener = listener;
            this.workerQueue = workerQueue;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var workItem = await workerQueue.TryDequeueAsync(cancellationToken);
                try
                {
                    var result = await workItem(cancellationToken);
                    workerQueue.SignalProcessed(result);
                }
                catch (Exception ex)
                {
                    workerQueue.SignalFailed(ex);
                }
            }
        }
    }
}