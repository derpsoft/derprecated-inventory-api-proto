namespace Derprecated.Api.Models.Routing
{
    public class QuantityOnHandResponse
    {
        public decimal Quantity { get; set; }
        public string UnitOfMeasure { get; set; } = "each";
    }
}
