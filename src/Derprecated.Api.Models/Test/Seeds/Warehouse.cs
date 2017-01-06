namespace Derprecated.Api.Models.Test.Seeds
{
    public static class Warehouse
    {
        public static readonly Models.Warehouse EmptyWarehouse = new Models.Warehouse
        {
            Id = 1,
            Name = "Test Warehouse"
        };

        public static readonly Models.Warehouse ElMonteWarehouse = new Models.Warehouse
        {
            Id = 2,
            Name = "El Monte"
        };
    }
}
