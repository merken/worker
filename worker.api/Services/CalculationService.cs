using System.Threading.Tasks;
using Worker.Api.Models;

namespace Worker.Api.Services
{

    public interface ICalculationService
    {
        Task<Calculation> Calculate(Calculation calculation);
    }

    public class CalculationService : ICalculationService
    {
        public async Task<Calculation> Calculate(Calculation calculation)
        {
            await Task.Delay(1000);

            switch (calculation.Type)
            {
                case CalculationType.Addition:
                    calculation.Result = calculation.A + calculation.B;
                    break;
                case CalculationType.Division:
                    calculation.Result = calculation.A / calculation.B;
                    break;
                case CalculationType.Multiplication:
                    calculation.Result = calculation.A * calculation.B;
                    break;
                case CalculationType.Subtraction:
                    calculation.Result = calculation.A - calculation.B;
                    break;
            }

            return calculation;
        }
    }
}