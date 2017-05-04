using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silk2WavCommon.FileOperation
{
    public class PcmReader
    {
        public static Byte[] GetFileBytesViaPath(string mypath)
        {
            var bytes = File.ReadAllBytes(mypath);
            return bytes;
        }
    }
}
