using Silk2WavCommon.Exceptions;
using Silk2WavCommon.Silk2WavConverter;
using SpeechWithLuis.Src.Model;
using SpeechWithLuis.Src.Services;
using SpeechWithLuis.Src.Static;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SpeechWithLuis.Controllers
{
    public class SilkController : ApiController
    {
        private SpeechRestService speechRestService = InstanceFactory.SpeechRestService;

        private LuisService luisService = InstanceFactory.LuisService;

        private SpeechService speechService = InstanceFactory.CreateSpeechServiceWithLocale();

        [HttpPost]
        public async Task<dynamic> Post([FromBody]byte[] audioSource, string locale, bool withIntent = true)
        {
            long tsWhenGetAudioText = 0;
            long tsWhenGetAudioIntention = 0;
            var arrivalTime = DateTime.UtcNow;
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            try
            {
                var silk2Wav = new Silk2Wav(audioSource, audioSource.Count<byte>());
                var outs = speechRestService
                    .UseLocale(locale)
                    .SendAudio(silk2Wav.WavBytes, silk2Wav.WavBytesLen);
                var result = outs.results[0];
                string lexical = result.name;
                string content = result.lexical;
                /*
                var outs = await speechService.ReconizeAudioStreamAsync(new MemoryStream(audioSource));
                string lexical = outs.DisplayText;
                */
                stopWatch.Stop();
                tsWhenGetAudioText = stopWatch.ElapsedMilliseconds;
                dynamic intentions = null;

                if (withIntent)
                {
                    stopWatch.Restart();
                    intentions = await luisService.GetIntention(lexical);
                    stopWatch.Stop();
                    tsWhenGetAudioIntention = stopWatch.ElapsedMilliseconds;
                }

                return new ResponeModel
                {
                    Text = content,
                    intentions = intentions,
                    GetAudioTextLantency = tsWhenGetAudioText,
                    GetAudioIntentionLantency = tsWhenGetAudioIntention,
                    ErrorCode = 0,
                    ErrorMessage = "Status OK.",
                    ArrivalTime = arrivalTime,
                    EndTime = DateTime.UtcNow
                };
            }
            catch(BaseException e)
            {
                return new ResponeModel
                {
                    Text = "",
                    intentions = null,
                    GetAudioTextLantency = 0,
                    GetAudioIntentionLantency = 0,
                    ErrorCode = e.ErrorCode,
                    ErrorMessage = e.Message,
                    ArrivalTime = arrivalTime,
                    EndTime = DateTime.UtcNow
                };
            }
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