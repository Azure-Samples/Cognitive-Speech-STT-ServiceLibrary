using SpeechLuisOwin.Src.AuthorizationProvider;
using SpeechLuisOwin.Src.Services;

namespace SpeechLuisOwin.Src.Static
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