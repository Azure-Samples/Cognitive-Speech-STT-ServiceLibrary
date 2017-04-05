using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechConsole.FileOperation
{
    public class PcmWriter
    {
        public static string path = "test.pcm"; 
        public static void WriteShorts(short[] values)
        {
            using (FileStream fs = new FileStream(path, FileMode.CreateNew, FileAccess.Write))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    foreach (short value in values)
                    {
                        bw.Write(value);
                    }
                }
            }
        }
    }
}
