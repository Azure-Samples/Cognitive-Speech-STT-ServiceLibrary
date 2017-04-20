using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Silk2WavCommon.FileOperation;
using SpeechLuisOwin.Test.Web;
using SpeechLuisOwin.Test.FileOperation;
using System.Diagnostics;

namespace SpeechLuisOwin.Test.SilkTest
{
    [TestClass]
    public class RemoteUnitTest
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            string remoteAddress = "https://opgwebsilk.azurewebsites.net";
            var client = new HttpClient
            {
                BaseAddress = new Uri(remoteAddress),
            };

            var req = await client.GetAsync("/api/Azure");
            //var req =   await server.CreateRequest("/api/Azure").GetAsync();
            var token = JsonConvert.DeserializeObject<string>(await req.Content.ReadAsStringAsync());
            var bytes = PcmReaderExt.GetFileBytes("phpRX7A4h.silk");
            var count = 40;
            Stopwatch stopWatch = new Stopwatch();
            
            while (count > 0)
            {
                stopWatch.Restart();
                var outs = AudioPost.SendAudioFile(bytes, remoteAddress + "/api/Silk?locale=zh-cn&withIntent=false", token);
                //var outs = await req1.Content.ReadAsStringAsync();
                
                --count;
                stopWatch.Stop();
                Console.WriteLine(outs + "  Elapse: " + stopWatch.ElapsedMilliseconds);
            }
            
            //System.Diagnostics.de.WriteLine(outs);
            Assert.AreEqual(1, 1);
        }
    }
}
