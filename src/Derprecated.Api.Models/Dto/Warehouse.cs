namespace Derprecated.Api.Models.Dto
{
    using ServiceStack;

    public class Warehouse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public static Warehouse From(Models.Warehouse source)
        {
            return new Warehouse().PopulateWith(source);
        }
    }
}
