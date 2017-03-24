using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;
using SpeechWithLuis.Src.Model;
using SpeechWithLuis.Src.Services;
using SpeechWithLuis.Src.Static;
using System;

namespace SpeechWithLuis.Controllers
{
    public class MediaController : ApiController
    {
        private SpeechRestService speechRestService = InstanceFactory.SpeechRestService;

        private LuisService luisService = InstanceFactory.LuisService;

        private SpeechService speechService = InstanceFactory.CreateSpeechServiceWithLocale();

        [HttpPost]
        public async Task<dynamic> Post([FromBody]byte[] audioSource, string locale)
        {
            long tsWhenGetAudioText = 0;
            long tsWhenGetAudioIntention = 0;
            var arrivalTime = DateTime.UtcNow;
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var outs = await speechRestService.SendAudio(new MemoryStream(audioSource));
            var result= outs.results[0];
            string lexical = result.name;
            /*
            var outs = await speechService.ReconizeAudioStreamAsync(new MemoryStream(audioSource));
            string lexical = outs.DisplayText;
            */
            stopWatch.Stop();
            tsWhenGetAudioText = stopWatch.ElapsedMilliseconds;
            stopWatch.Restart();
            var intentions = await luisService.GetIntention(lexical);
            stopWatch.Stop();
            tsWhenGetAudioIntention = stopWatch.ElapsedMilliseconds;

            return new ResponeModel
            {
                Text = lexical,
                intentions = intentions,
                GetAudioTextLantency = tsWhenGetAudioText,
                GetAudioIntentionLantency = tsWhenGetAudioIntention,
                ArrivalTime = arrivalTime,
                EndTime = DateTime.UtcNow
            };
        }

        [HttpGet]
        public async Task<ResponeModel> Get(string id) 
        {
            await Task.Delay(100);
            return new ResponeModel
            {
                Text = "test",
                intentions = "test01"
            };
        }
    }
}