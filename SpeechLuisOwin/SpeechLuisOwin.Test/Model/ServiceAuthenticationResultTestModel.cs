using System;

namespace SpeechLuisOwin.Test.Model
{
    public class ServiceAuthenticationResultTestModel
    {
        public string AccessToken { get; set; }

        public DateTimeOffset ExpiresOn { get; set; }
    }
}
