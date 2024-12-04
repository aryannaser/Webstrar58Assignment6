using System.Net.Http.Headers;
using System.Web.Http;

namespace Assignment6CSE445
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Force JSON response formatting
            config.Formatters.JsonFormatter.SupportedMediaTypes
                  .Add(new MediaTypeHeaderValue("text/html"));

            // Enable attribute-based routing
            config.MapHttpAttributeRoutes();

            // Default route
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
