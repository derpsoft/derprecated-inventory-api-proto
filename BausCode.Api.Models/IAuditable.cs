using System;

namespace BausCode.Api.Models
{
    public interface IAuditable
    {
        DateTime CreateDate { get; set; }
        DateTime ModifyDate { get; set; }
        ulong RowVersion { get; set; }
    }
}