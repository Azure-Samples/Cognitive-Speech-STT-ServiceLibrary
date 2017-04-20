using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Flurl.Http;
using SpeechConsole.Static;
using System.IO;

namespace SpeechConsole.Web
{
    public class AudioPost
    {

        //static string URI = "https://opgwebsilk.chinacloudsites.cn/api/Silk?locale=zh-cn&withIntent=false";
        //static string URI = "https://opgwebsilk.azurewebsites.net/api/Silk?locale=zh-cn&withIntent=false";
        //static string URI = "https://opgwebsilk-opgwebsilkslo.azurewebsites.net/api/Silk?locale=zh-cn&withIntent=false";
        static string URI = "https://opgwebsilkhk.azurewebsites.net/api/Silk?locale=zh-cn&withIntent=false";


        public static string SendAudioFile(Byte[] bytes, int count)
        {
            string _ContentType = @"audio/wav; codec=""audio/pcm""; samplerate=16000";
            var request = (HttpWebRequest)HttpWebRequest.Create(URI);
            request.SendChunked = true;
            request.Accept = @"application/json;text/xml";
            request.Method = "POST";
            //request.ProtocolVersion = HttpVersion.Version11;
            //request.Host = host;
            request.ContentType = _ContentType;
            request.ContentLength = count;
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(bytes, 0, count);
                // Flush
                requestStream.Flush();
            }

            var responseString = "";
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                    {
                        responseString = sr.ReadToEnd();
                    }

                    //Console.WriteLine(responseString);
                }

            }
            catch (Exception e)
            {
                throw e;
            }


            return responseString;

        }
    }
}
