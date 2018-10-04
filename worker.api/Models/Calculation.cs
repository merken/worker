using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Worker.Api.Models
{
    public enum CalculationType
    {
        Addition,
        Subtraction,
        Division,
        Multiplication
    }

    public class Calculation
    {
        [JsonConverter(typeof(StringEnumConverter), new object[] { true })]
        public CalculationType Type { get; set; }
        public double A { get; set; }
        public double B { get; set; }
        public double Result { get; set; }
    }
}