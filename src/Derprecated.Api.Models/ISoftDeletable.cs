namespace Derprecated.Api.Models
{
    using System;

    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }
        DateTime DeleteDate { get; set; }
    }
}
