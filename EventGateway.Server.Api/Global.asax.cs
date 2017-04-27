using System.Web.Http;
using EventGateway.Server.Api.App_Start;

namespace EventGateway.Server.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
