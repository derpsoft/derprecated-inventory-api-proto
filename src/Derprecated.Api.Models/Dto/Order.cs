namespace Derprecated.Api.Models.Dto
{
    using ServiceStack;

    public class Order
    {
        public int Id { get; set; }

        public static Order From(Models.Order source)
        {
            return new Order().PopulateWith(source);
        }
    }
}
