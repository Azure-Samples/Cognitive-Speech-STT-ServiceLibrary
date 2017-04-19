using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Owin.Hosting;
using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace SpeechLuisOwin.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            string baseAddress = "http://localhost:5000/";
            // using (var server = TestServer.Create<Startup>())
            using (WebApp.Start<Startup>(baseAddress))
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri(baseAddress),
                };

                var r = await client.GetAsync("/Api/Values");
                var array = JsonConvert.DeserializeObject<string[]>(await r.Content.ReadAsStringAsync());
                Assert.AreEqual(2, array.Length);
            }
        }


    }
}
