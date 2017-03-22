using SpeechWithLuis.Src.Model;
using SpeechWithLuis.Src.Services;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;

namespace SpeechWithLuis.Controllers
{
    public class MediaController : ApiController
    {
        private SpeechRestService speechRestService = new SpeechRestService();

        private LuisService luisService = new LuisService();

        [HttpPost]
        public async Task<dynamic> Post([FromBody]byte[] audioSource)
        {
            var outs = await speechRestService.SendAudio(new MemoryStream(audioSource));
            var result= outs.results[0];
            string lexical = result.name;
            var intentions = await luisService.GetIntention(lexical);

            //await Task.Delay(100);
            return intentions;
        }

        [HttpGet]
        public async Task<ResponeModel> Get(int id) 
        {
            await Task.Delay(100);
            return new ResponeModel
            {
                Text = "test",
                Intention = "test01"
            };
        }
    }
}