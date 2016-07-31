// ReSharper disable UnusedMember.Global

using ServiceStack.DataAnnotations;

namespace BausCode.Api.Jobs
{
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