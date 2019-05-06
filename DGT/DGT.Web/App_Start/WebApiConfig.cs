using Newtonsoft.Json.Serialization;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace DGT.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "Infracciones",
                routeTemplate: "api/infracciones/",
                defaults: new { controller = "Infracciones"}
            );

            config.Routes.MapHttpRoute(
               name: "Vehiculos",
               routeTemplate: "api/vehiculos/",
               defaults: new { controller = "Vehiculos"}
           );

            config.Routes.MapHttpRoute(
                  name: "Conductores",
                  routeTemplate: "api/conductores/{anio}",
                  defaults: new { controller = "Conductores", anio = RouteParameter.Optional }
              );

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
