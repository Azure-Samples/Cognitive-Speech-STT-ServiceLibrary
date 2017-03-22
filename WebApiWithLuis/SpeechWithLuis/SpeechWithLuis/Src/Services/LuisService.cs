using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace SpeechWithLuis.Src.Services
{
    public class LuisService
    {
        private static readonly string url = "https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/{0}?subscription-key={1}&verbose={2}&q={3}";

        private string subscriptionKey = "";

        private string appId = "";

        private bool verbose = true;

        //private string  text = "";

        public LuisService() : this("9ed0fa35-3fe7-4e85-8479-23fabb7aee93", "31d36350fe8e499d86f816dda9c86357") { }

        public LuisService(string appId, string subKey, bool verbose = true)
        {
            this.appId = appId;
            this.subscriptionKey = subKey;
            this.verbose = verbose;
        }



        public async Task<dynamic> GetIntention(string text)
        {
            using (var client = new HttpClient())
            {
                var reponse = await client.GetAsync(string.Format(LuisService.url, this.appId, this.subscriptionKey, this.verbose, text));
                reponse.EnsureSuccessStatusCode();
                string responseBody = await reponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<dynamic>(responseBody);
            }
        }

    }
}