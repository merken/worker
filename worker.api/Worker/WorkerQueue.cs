using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Worker.Api.Models;

namespace Worker.Api.Worker
{
    public class WorkerQueue
    {
        public event EventHandler<object> WorkItemProcessed;
        public event EventHandler<Exception> WorkItemFailed;
        private SemaphoreSlim signal = new SemaphoreSlim(0);
        private readonly ConcurrentQueue<Func<CancellationToken, Task<object>>> queue = new ConcurrentQueue<Func<CancellationToken, Task<object>>>();

        public WorkerQueue(IServiceProvider serviceProvider) { }

        public void Enqueue(Func<CancellationToken, Task<object>> workItem)
        {
            queue.Enqueue(workItem);
            signal.Release();
        }

        internal void SignalProcessed(object result)
        {
            if (this.WorkItemProcessed != null)
            {
                this.WorkItemProcessed.Invoke(this, result);
            }
        }
        
        internal void SignalFailed(Exception ex)
        {
            if (this.WorkItemFailed != null)
            {
                this.WorkItemFailed.Invoke(this, ex);
            }
        }

        internal async Task<Func<CancellationToken, Task<object>>> TryDequeueAsync(CancellationToken cancellationToken)
        {
            await signal.WaitAsync(cancellationToken);
            queue.TryDequeue(out var workItem);

            return workItem;
        }
    }
}