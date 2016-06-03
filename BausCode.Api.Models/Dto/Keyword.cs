using System;

namespace BausCode.Api.Models.Dto
{
    public class Keyword
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public DateTime CreateDate { get; set; }

        public static Keyword From(Models.Keyword source)
        {
            var keyword = new Keyword();

            keyword.Id = source.Id;
            keyword.Value = source.Value;
            keyword.CreateDate = source.CreateDate;

            return keyword;
        }
    }
}