using System;
using System.Net;
using System.IO;

namespace SpeechLuisOwin.Test.Web
{
    public class AudioPost
    {

        //static string URI = "https://opgwebsilk.chinacloudsites.cn/api/Silk?locale=zh-cn&withIntent=false";
        static string URI = "https://opgwebsilk.azurewebsites.net/api/Silk?locale=zh-cn&withIntent=false";
        //static string URI = "https://opgwebsilk-opgwebsilkslo.azurewebsites.net/api/Silk?locale=zh-cn&withIntent=false";

        public static string SendAudioFile(Byte[] bytes, int count, string token = "")
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
            request.Headers["Authorization"] = "Bearer " + token;
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

        public static string SendAudioFile(Byte[] bytes, string url, string token = "")
        {
            URI = url;
            var len = bytes.GetLength(0);
            return SendAudioFile(bytes, len, token);
        }
    }
}
