using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BausCode.Api.Configuration;
using BausCode.Api.Services;

namespace BausCode.Api.Host
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var app = new Application("Web", typeof(BaseService).Assembly);
            app.Init();
        }
    }
}
