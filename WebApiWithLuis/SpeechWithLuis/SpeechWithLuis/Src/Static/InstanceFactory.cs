using SpeechWithLuis.Src.AuthorizationProvider;
using SpeechWithLuis.Src.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeechWithLuis.Src.Static
{
    public class InstanceFactory
    {
        public static Authentication Authentication = new Authentication(Configurations.speechSubKey);

        public static SpeechRestService SpeechRestService = new SpeechRestService();

        public static LuisService LuisService = new LuisService();

        public static SpeechService CreateSpeechServiceWithLocale(string locale = "zh-cn")
        {
            return new SpeechService(locale);
        }
    }
}