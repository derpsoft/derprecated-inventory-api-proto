namespace BausCode.Api.Models
{
    using System;

    public interface IAuditable
    {
        DateTime CreateDate { get; set; }
        DateTime ModifyDate { get; set; }
        ulong RowVersion { get; set; }
    }
}
