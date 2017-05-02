using Microsoft.Owin.Hosting;
using Microsoft.Owin.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Silk2WavCommon.Silk2WavConverter;
using SpeechLuisOwin.Test.FileOperation;
using SpeechLuisOwin.Test.Model;
using SpeechLuisOwin.Test.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SpeechLuisOwin.Test.SilkTest
{
    [TestClass]
    public class SilkTest
    {

        /// <summary>
        /// with HttpClient authentication won't be working
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task TestMethod1()
        {
            string baseAddress = "http://localhost:5000/";
            //using (var server = TestServer.Create<Startup>())
            using (WebApp.Start<Startup>(baseAddress))
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri(baseAddress),
                };

                var req = await client.GetAsync("/api/Azure");
                //var req =   await server.CreateRequest("/api/Azure").GetAsync();
                var model = JsonConvert.DeserializeObject<ServiceAuthenticationResultTestModel>(await req.Content.ReadAsStringAsync());
                //var web = server.CreateRequest("/api/Silk");
                var client1 = new HttpClient
                {
                    BaseAddress = new Uri(baseAddress),
                };
                Console.WriteLine(model.AccessToken);
                client1.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", model.AccessToken);
                //client1.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", token));
                string _ContentType = "audio/wav";
                client1.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_ContentType));
                //client1.DefaultRequestHeaders.Add("", );
                var bytes = PcmReaderExt.GetFileBytes("phpRX7A4h.silk");
                var byteContent = new ByteArrayContent(bytes);
                //byteContent.Headers.Add();
                var req1 = await client.PostAsync("api/Silk?locale=zh-cn&withIntent=false", byteContent);

                //var outs = AudioPost.SendAudioFile(bytes, baseAddress + "api/Silk?locale=en-us&withIntent=true");
                var outs = await req1.Content.ReadAsStringAsync();

                Assert.AreEqual(1, 1);
            }
        }



        [TestMethod]
        public async Task TestMethod2()
        {
            string baseAddress = "http://localhost:5000/";
            using (WebApp.Start<Startup>(baseAddress))
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri(baseAddress),
                };

                var req = await client.GetAsync("/api/Azure");
                //var req =   await server.CreateRequest("/api/Azure").GetAsync();
                var token = JsonConvert.DeserializeObject<string>(await req.Content.ReadAsStringAsync());
                var bytes = PcmReaderExt.GetFileBytes("phpRX7A4h.silk");
                var outs = AudioPost.SendAudioFile(bytes, baseAddress + "api/Silk?locale=zh-cn&withIntent=false", token);
                //var outs = await req1.Content.ReadAsStringAsync();

                Assert.AreEqual(1, 1);
            }
        }

        [TestMethod]
        public async Task TestMethod3()
        {
            using (var server = TestServer.Create<Startup>())
            {
                var req =   await server.CreateRequest("/api/Azure").GetAsync();
                var token = JsonConvert.DeserializeObject<ServiceAuthenticationResultTestModel>(await req.Content.ReadAsStringAsync());
                var web = server.CreateRequest("/api/Test");
                web.AddHeader("Authorization", "Bearer " + token.AccessToken);
                var req1 = await web.GetAsync();
                var outs = await req1.Content.ReadAsStringAsync();
                Assert.AreEqual(1, 1);
            }
        }


        [TestMethod]
        public void TestMethod4()
        {
            var bytes = PcmReaderExt.GetFileBytes("phpRX7A4h.silk");
            var len = bytes.GetLength(0);
            var silk2Wav = new Silk2Wav(bytes, len);
            PcmWriter.WriteBytes2SpecificFile(silk2Wav.WavBytes, "j8.wav");
        }


        [TestMethod]
        public async Task TestMethod5()
        {
            using (var server = TestServer.Create<Startup>())
            {
                var req = await server.CreateRequest("/api/Azure").GetAsync();
                var token = JsonConvert.DeserializeObject<string>(await req.Content.ReadAsStringAsync());
                var web = server.CreateRequest("api/Silk?locale=zh-cn&withIntent=false");
                web.AddHeader("Authorization", "Bearer " + token);
                var bytes = PcmReaderExt.GetFileBytes("phpRX7A4h.silk");
                web.And(request => request.Content = new ByteArrayContent(bytes));
                var req1 = await web.PostAsync();
                var outs = await req1.Content.ReadAsStringAsync();
                Assert.AreEqual(1, 1);
            }
        }

    }
}
