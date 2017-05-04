using Microsoft.Bing.Speech;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interface.IService
{
    public interface ISpeechService
    {
        Task<RecognitionPhrase> ReconizeAudioStreamAsync(Stream audioStream);
    }
}
