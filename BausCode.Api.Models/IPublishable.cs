using System;

namespace BausCode.Api.Models
{
    public interface IPublishable
    {
        DateTime PublishDate { get; }
    }
}