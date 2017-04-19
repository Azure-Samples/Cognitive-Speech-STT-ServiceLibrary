using System.Net.Http.Headers;
using System.Web.Http;
using Microsoft.Owin;
using Owin;
using SpeechLuisOwin.Src.Ext;
using SpeechLuisOwin.Src.Media;

[assembly: OwinStartup(typeof(SpeechLuisOwin.Startup))]
namespace SpeechLuisOwin
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseAuth();
            app.UseRoute((config) => {               
                config.MapHttpAttributeRoutes();
                config.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "api/{controller}/{id}",
                    defaults: new { id = RouteParameter.Optional }
                );

                config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
                config.Formatters.Add(new BinaryMediaTypeFormatter());
                return config;
            });
        }
    }
}