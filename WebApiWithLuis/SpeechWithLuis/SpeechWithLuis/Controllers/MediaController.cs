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

        [HttpPost]
        public async Task<dynamic> Post([FromBody]byte[] audioSource)
        {
            var arrivalTime = DateTime.UtcNow.ToLongDateString();
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var outs = await speechRestService.SendAudio(new MemoryStream(audioSource));
            var result= outs.results[0];
            string lexical = result.name;
            var intentions = await luisService.GetIntention(lexical);

            //await Task.Delay(100);
            return new ResponeModel
            {
                ArrivalTime = arrivalTime,
                intentions = intentions,
                EndTime = DateTime.UtcNow.ToLongDateString()
            }; ;
        }

        [HttpGet]
        public async Task<ResponeModel> Get(int id) 
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