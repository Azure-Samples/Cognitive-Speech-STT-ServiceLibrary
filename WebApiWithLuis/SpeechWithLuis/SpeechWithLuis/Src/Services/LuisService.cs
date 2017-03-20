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

        private bool verbose = true;

        private string  text = "";

        public LuisService(string subKey, bool verbose)
        {
            this.subscriptionKey = subKey;
            this.verbose = verbose;
        }

        public async Task<dynamic> GetIntention()
        {
            using (var client = new HttpClient())
            {
                var reponse = await client.GetAsync(string.Format(LuisService.url, this.subscriptionKey, this.verbose, this.text));
                reponse.EnsureSuccessStatusCode();
                string responseBody = await reponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<dynamic>(responseBody);
            }
        }

    }
}