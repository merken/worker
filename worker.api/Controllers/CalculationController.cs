using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Worker.Api.Worker;
using Worker.Api.Models;
using Worker.Api.Services;

namespace Worker.Api.Controllers
{
    [Route("api/calculation")]
    [ApiController]
    public class CalculationController : ControllerBase
    {
        private readonly WorkerQueue workerQueue;
        private readonly ICalculationService calculationService;

        public CalculationController(WorkerQueue workerQueue, ICalculationService calculationService)
        {
            this.calculationService = calculationService;
            this.workerQueue = workerQueue;
        }

        [HttpPost]
        public IActionResult Calculate(Calculation calculation)
        {
            workerQueue.Enqueue((token) => CalculateInternal(calculation));
            return Ok();
        }

        private async Task<object> CalculateInternal(Calculation calculation)
        {
            var result = await this.calculationService.Calculate(calculation);
            return result;
        }
    }
}
