using SpeechWithLuis.Src.Model;
using SpeechWithLuis.Src.Services;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;

namespace SpeechWithLuis.Controllers
{
    public class MediaController : ApiController
    {
        [HttpPost]
        public async Task<ResponeModel> Post([FromBody]byte[] audioSource)
        {
            var service = new SpeechRestService();
            var outs = await service.SendAudio(new MemoryStream(audioSource));
            await Task.Delay(100);
            return new ResponeModel
            {
                Text = "test",
                Intention = "test01"
            };
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