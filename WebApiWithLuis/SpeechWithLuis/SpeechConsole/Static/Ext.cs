using Flurl;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SpeechConsole.Static
{
    public static class Ext
    {
        public static Task<HttpResponseMessage> PostFileAsync(this FlurlClient client, byte[] data)
        {
            string _ContentType = @"audio/wav; codec=""audio/pcm""; samplerate=16000";
            var content = new ByteArrayContent(data);
            content.Headers.Add("Content-Type", _ContentType);
            //content.Headers.Add("Content-Length", data.Length);
            return client.SendAsync(HttpMethod.Post, content: content);
        }

        public static Task<HttpResponseMessage> PostFileAsync(this Url url, byte[] data)
        {
            return new FlurlClient(url).PostFileAsync(data);
        }

        public static Task<HttpResponseMessage> PostFileAsync(this string url, byte[] data)
        { 
            return new FlurlClient(url).PostFileAsync(data);
        }
    }
}
