using Microsoft.Owin.Security.ActiveDirectory;
using Owin;
using SpeechLuisOwin.Src.Static;
using System.IdentityModel.Tokens;

namespace SpeechLuisOwin.Src.Ext
{
    public static class Auth
    {
        public static void UseAuth(this IAppBuilder app)
        {
            app.UseWindowsAzureActiveDirectoryBearerAuthentication(
                new WindowsAzureActiveDirectoryBearerAuthenticationOptions
                {
                    // Audience = Configurations.aad_ClientId,
                    Tenant = Configurations.aad_Tenant,
                    TokenValidationParameters = new TokenValidationParameters
                    {
                        // wrong here because Client ID is not setted as audience
                        // {
                        //  "typ": "JWT",
                        //  "alg": "RS256",
                        //  "x5t": "a3QN0BZS7s4nN-BdrjbF0Y_LdMM",
                        //  "kid": "a3QN0BZS7s4nN-BdrjbF0Y_LdMM"
                        //  }

                        //  {
                        //  "aud": "https://graph.windows.net",
                        //  "iss": "https://sts.windows.net/72f988bf-86f1-41af-91ab-2d7cd011db47/",
                        //  "iat": 1492573665,
                        //  "nbf": 1492573665,
                        //  "exp": 1492577565,
                        //  "aio": "Y2ZgYFhcweqluMMuwlFohrE1s/p9AA==",
                        //  "appid": "e1374b42-6391-4fc5-8942-2bd7135f1c32",
                        //  "appidacr": "1",
                        //  "e_exp": 262799,
                        //  "idp": "https://sts.windows.net/72f988bf-86f1-41af-91ab-2d7cd011db47/",
                        //  "oid": "850ce50a-defb-4079-b035-9b61d2ee26e9",
                        //  "sub": "850ce50a-defb-4079-b035-9b61d2ee26e9",
                        //  "tid": "72f988bf-86f1-41af-91ab-2d7cd011db47",
                        //  "uti": "uoTgpW_njk-o_JJGMA0IAA",
                        //  "ver": "1.0"
                        //  }

                        // ValidAudience = Configurations.aad_ClientId
                        ValidAudience = Configurations.aad_Resource
                    }
                });
        }
    }
}