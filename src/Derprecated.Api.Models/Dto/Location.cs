namespace Derprecated.Api.Models.Dto
{
    using ServiceStack;

    public class Location
    {
        public string Bin { get; set; }
        public int Id { get; set; }
        public string Rack { get; set; }
        public string Row { get; set; }
        public string Shelf { get; set; }

        public static Location From(Models.Location source)
        {
            return new Location().PopulateWith(source);
        }
    }
}
