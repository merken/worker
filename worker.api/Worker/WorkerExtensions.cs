using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Worker.Api.Models;
using Worker.Api.Services;

namespace Worker.Api.Worker
{
    public static class WorkerExtensions
    {
        public static IServiceCollection UseCalculationServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ICalculationService, CalculationService>();
            serviceCollection.AddSingleton<WorkerQueue>();
            serviceCollection.AddSingleton<WorkerListener>();
            serviceCollection.AddSingleton<IHostedService, WorkerService>();

            return serviceCollection;
        }
    }
}