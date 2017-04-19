using Owin;
using System;
using System.Web.Http;

namespace SpeechLuisOwin.Src.Ext
{
    public static class Http
    {
        public static void UseRoute(this IAppBuilder app, Func<HttpConfiguration, HttpConfiguration> action)
        {
            var config = new HttpConfiguration();
            var con = action(config);
            app.UseWebApi(con);
        }
    }
}