using Newtonsoft.Json;
using SpeechWithLuis.Src.AuthorizationProvider;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace SpeechWithLuis.Src.Services
{
    public class SpeechRestService
    {
        private static string baseUri = "https://speech.platform.bing.com/recognize";

        private static string uriForUsing;

        static SpeechRestService()
        {
            string requestUri = baseUri.Trim(new char[] { '/', '?' });

            /* URI Params. Refer to the README file for more information. */
            requestUri += @"?scenarios=smd";                                  // websearch is the other main option.
            requestUri += @"&appid=D4D52672-91D7-4C74-8AD8-42B1D98141A5";     // You must use this ID.
            requestUri += @"&locale=en-US";                                   // We support several other languages.  Refer to README file.
            requestUri += @"&device.os=wp7";
            requestUri += @"&version=3.0";
            requestUri += @"&format=json";
            requestUri += @"&instanceid=565D69FF-E928-4B7E-87DA-9A750B96D9E3";
            requestUri += @"&requestid=" + Guid.NewGuid().ToString();

            uriForUsing = requestUri;
        }

       
        private string host = @"speech.platform.bing.com";

        private string contentType = @"audio/wav; codec=""audio/pcm""; samplerate=16000";

        private string token;
        

        public SpeechRestService()
        {
            //Authentication auth = new Authentication(subKey);
            token = Authentication.auth.GetAccessToken();
        }

        public async Task<dynamic> SendAudio(Stream stream)
        {
            HttpContent content = new StreamContent(stream);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var reponse = await client.PostAsync(uriForUsing, content);
                reponse.EnsureSuccessStatusCode();
                string responseBody = await reponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<dynamic>(responseBody);
            }

            //return null;
        }

    }
}