using System;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Worker.Api.Hubs;
using Worker.Api.Models;

namespace Worker.Api.Worker
{
    public class WorkerListener
    {
        private readonly IHubContext<WorkerHub> hubcontext;
        private readonly WorkerQueue queue;
        private readonly ILogger<WorkerListener> logger;
        public WorkerListener(WorkerQueue queue, IHubContext<WorkerHub> hubcontext, ILogger<WorkerListener> logger)
        {
            this.logger = logger;
            this.queue = queue;
            this.hubcontext = hubcontext;
            this.queue.WorkItemProcessed += this.OnWorkItemProcessed;
            this.queue.WorkItemFailed += this.OnWorkItemFailed;
        }

        private void OnWorkItemProcessed(object sender, object result)
        {
            var calculation = result as Calculation;
            if (calculation != null)
            {
                this.hubcontext.Clients.All
                    .SendAsync("WORK_ITEM_PROCESSED", calculation)
                    .GetAwaiter()
                    .GetResult();
            }
        }

        private void OnWorkItemFailed(object sender, Exception e)
        {
            logger.LogError(new EventId(), e, e.Message);
        }
    }
}