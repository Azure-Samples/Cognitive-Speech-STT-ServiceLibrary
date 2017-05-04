using Common.Interface.IService;
using Common.Service.Exceptions;
using Common.Service.Model;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SpeechLuisOwin.Src.Services
{
    public class LuisService : ILuisService
    {
        private static readonly string url = "https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/{0}?subscription-key={1}&verbose={2}&q={3}";

        private string subscriptionKey = "";

        private string appId = "";

        private bool verbose = true;

        //private string  text = "";

        private LuisModel _luisModel;



        public LuisService(LuisModel model) : this(model.LuisAppId, model.LuisSubKey)
        {
            _luisModel = model;
        }

        public LuisService(string appId, string subKey, bool verbose = true)
        {
            this.appId = appId;
            this.subscriptionKey = subKey;
            this.verbose = verbose;
        }

        async Task<dynamic> ILuisService.GetIntention(string text)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var reponse = await client.GetAsync(string.Format(LuisService.url, this.appId, this.subscriptionKey, this.verbose, text));
                    reponse.EnsureSuccessStatusCode();
                    string responseBody = await reponse.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<dynamic>(responseBody);
                }
                catch (Exception e)
                {
                    throw new LuisException(1001, "Luis Understanding Error!", e);
                }

            }
        }
    }
}