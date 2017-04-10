using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechConsole.FileOperation
{
    public class PcmReader
    {
        public static string path = @"phpBV6Z6a.silk";

        public static Byte[] GetFileBytes()
        {
            var bytes = File.ReadAllBytes(path);
            return bytes;
        }

        public static Byte[] GetFileBytes(string mypath)
        {
            var bytes = File.ReadAllBytes(mypath);
            return bytes;
        }
    }
}
