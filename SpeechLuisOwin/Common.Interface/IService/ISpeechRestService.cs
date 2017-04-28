using System.IO;

namespace Common.Interface.IService
{
    public interface ISpeechRestService
    {
        dynamic SendAudio(Stream stream);

        dynamic SendAudio(byte[] audioArray, int length);

        ISpeechRestService UseLocale(string locale);
    }
}
