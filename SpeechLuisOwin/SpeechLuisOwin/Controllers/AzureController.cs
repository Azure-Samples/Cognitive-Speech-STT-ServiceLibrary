
using SpeechLuisOwin.Src.AuthorizationProvider;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace SpeechLuisOwin.Controllers
{
    public class AzureController : ApiController
    {
        public async Task<string> Get()
        {
            var getter = new AADTokenProvider();
            return await getter.GetAccessTokenFromAAD();
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