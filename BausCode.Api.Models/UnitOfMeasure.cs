namespace BausCode.Api.Models
{
    using System;

    public class UnitOfMeasure : IAuditable
    {
        public DateTime CreateDate { get; set; }
        public int Id { get; set; }
        public DateTime ModifyDate { get; set; }
        public string Name { get; set; }
        public ulong RowVersion { get; set; }
    }
}
