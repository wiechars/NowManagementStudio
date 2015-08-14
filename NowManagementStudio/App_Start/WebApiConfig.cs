using Newtonsoft.Json.Serialization;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Linq;

public static class WebApiConfig
{
    public static void Register(System.Web.Http.HttpConfiguration config)
    {

        // Web API routes
        config.MapHttpAttributeRoutes();

        config.Routes.MapHttpRoute(
            name: "DefaultApi",
            routeTemplate: "api/{controller}/{id}",
            defaults: new { id = RouteParameter.Optional }
        );

        var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
        jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    }
}