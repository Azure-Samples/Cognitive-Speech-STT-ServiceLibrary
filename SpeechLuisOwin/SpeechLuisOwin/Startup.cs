using Microsoft.Owin;
using Microsoft.Owin.Security.ActiveDirectory;
using Owin;
using SpeechLuisOwin.Src.Static;
using System.Configuration;
using System.IdentityModel.Tokens;
using System.Web.Http;

[assembly: OwinStartup(typeof(SpeechLuisOwin.Startup))]
namespace SpeechLuisOwin
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseWindowsAzureActiveDirectoryBearerAuthentication(
                new WindowsAzureActiveDirectoryBearerAuthenticationOptions
                {
                    // Audience = Configurations.aad_ClientId,
                    Tenant = Configurations.aad_Tenant,
                    TokenValidationParameters = new TokenValidationParameters
                    {
                        // wrong here because Client ID is not setted as audience
                        // ValidAudience = Configurations.aad_ClientId
                        ValidAudience = Configurations.aad_Resource
                    }
                });
        }

    }
}