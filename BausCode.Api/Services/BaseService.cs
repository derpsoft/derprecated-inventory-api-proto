using BausCode.Api.Models;
using ServiceStack;

namespace BausCode.Api.Services
{
    public abstract class BaseService : Service
    {
        public Configuration Configuration { get; set; }
    }
}