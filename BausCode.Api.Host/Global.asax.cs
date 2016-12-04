namespace BausCode.Api.Host
{
    using System.Web;
    using Configuration;
    using Services;

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            var app = new Application("Web", typeof (BaseService).Assembly);
            app.Init();
        }
    }
}
