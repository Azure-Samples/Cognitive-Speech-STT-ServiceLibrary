using Common.Service.AuthorizationProvider;
using Common.Service.Model;
using System.Threading.Tasks;
using System.Web.Http;

namespace SpeechLuisOwin.Controllers
{
    public class AzureController : ApiController
    {
        private AADTokenProvider _tokenProvider;

        public AzureController(AADTokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider;
        }

        public async Task<ServiceAuthenticationResultModel> Get()
        {
            //var getter = new AADTokenProvider();
            return await _tokenProvider.GetAccessTokenFromAAD();
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}