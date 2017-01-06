namespace Derprecated.Api.Models.Dto
{
    using ServiceStack;

    public class Dto<T>
    {
        public ResponseStatus ResponseStatus { get; set; }
        public T Result { get; set; }
    }
}
