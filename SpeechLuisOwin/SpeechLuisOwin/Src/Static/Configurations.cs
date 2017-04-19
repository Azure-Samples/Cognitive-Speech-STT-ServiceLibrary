using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SpeechLuisOwin.Src.Static
{
    public class Configurations
    {
        public static string luisAppId = "8a3aeb1c-525c-44c9-9be9-856b3b35fe53";

        public static string luisSubKey = "1ccb9a5dfc844d21b91f762b7d07e5ef";

        public static string speechSubKey = "1da1bed1e00a46c5a3a953235417381c";
        
        public static string aad_Tenant = ConfigurationManager.AppSettings["ida:Tenant"];

        public static string aad_Audience = ConfigurationManager.AppSettings["ida:Audience"];

        public static string aad_ClientId = ConfigurationManager.AppSettings["ida:ClientId"];

        public static string aad_AuthUri = ConfigurationManager.AppSettings["ida:AuthUri"];

        public static string aad_Key = ConfigurationManager.AppSettings["ida:Key"];

        public static string aad_Resource = ConfigurationManager.AppSettings["ida:Resource"];
    }
}