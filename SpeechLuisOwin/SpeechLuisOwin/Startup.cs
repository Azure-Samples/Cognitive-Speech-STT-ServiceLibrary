using System.Net.Http.Headers;
using System.Web.Http;
using Owin;
using SpeechLuisOwin.Src.Ext;
using SpeechLuisOwin.Src.Media;
using SpeechLuisOwin.Ioc;
using SpeechLuisOwin.Src.Ioc;
using Microsoft.Owin;
using Microsoft.Extensions.DependencyInjection;
using SpeechLuisOwin.Src.AuthorizationProvider;
using System.Web.Http.Dependencies;

[assembly: OwinStartup(typeof(SpeechLuisOwin.Startup))]
namespace SpeechLuisOwin
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // ioc part
            IocHelper.BuildServiceProvider( services =>
            {
                services.AddTransient<AADTokenProvider>();
                return services;
            });

            // middleware part
            app.UseAuth();
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseRoute((config) => {               
                config.MapHttpAttributeRoutes();
                config.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "api/{controller}/{id}",
                    defaults: new { id = RouteParameter.Optional }
                );

                config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
                config.Formatters.Add(new BinaryMediaTypeFormatter());

                config.DependencyResolver = new DefaultDependencyResolver(IocHelper.ServiceProvider);

                return config;
            });
        }
    }
}