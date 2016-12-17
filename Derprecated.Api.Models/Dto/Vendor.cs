namespace Derprecated.Api.Models.Dto
{
    using ServiceStack;

    public class Vendor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ulong RowVersion { get; set; }

        public static Vendor From(Models.Vendor source)
        {
            var result = new Vendor().PopulateWith(source);

            return result;
        }
    }
}
