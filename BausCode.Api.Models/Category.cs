namespace BausCode.Api.Models
{
    using System;

    public class Category : IAuditable
    {
        public DateTime CreateDate { get; set; }
        public int Id { get; set; }
        public DateTime ModifyDate { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public ulong RowVersion { get; set; }
        public int Sort { get; set; }
    }
}
