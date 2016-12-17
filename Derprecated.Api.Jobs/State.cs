// ReSharper disable UnusedMember.Global

namespace Derprecated.Api.Jobs
{
    using ServiceStack.DataAnnotations;

    public class State
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        [Required]
        [Index(Unique = true)]
        public string Key { get; set; }

        [Required]
        public string Value { get; set; }
    }
}
