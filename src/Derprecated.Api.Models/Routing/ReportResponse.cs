namespace Derprecated.Api.Models.Routing
{
    using ServiceStack;

    public class ReportResponse<T>
    {
        public T Report { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }
}
