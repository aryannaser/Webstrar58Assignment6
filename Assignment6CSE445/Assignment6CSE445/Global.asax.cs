using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http; // Required for Web API configuration

namespace Assignment6CSE445
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup

            // Register Web API configuration
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // Register routes for Web Forms
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // Register bundles (CSS/JS optimization)
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
