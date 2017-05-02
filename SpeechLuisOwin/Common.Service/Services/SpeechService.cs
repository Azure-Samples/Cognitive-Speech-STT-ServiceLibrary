
using Microsoft.Bing.Speech;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.Interface.IService;
using Common.Service.AuthorizationProvider;
using Common.Service.Model;

namespace SpeechLuisOwin.Src.Services
{
    public class SpeechService : ISpeechService
    {
        /// <summary>
        /// Short phrase mode URL
        /// </summary>
        private static readonly Uri ShortPhraseUrl = new Uri(@"wss://speech.platform.bing.com/api/service/recognition");

        /// <summary>
        /// The long dictation URL
        /// </summary>
        private static readonly Uri LongDictationUrl = new Uri(@"wss://speech.platform.bing.com/api/service/recognition/continuous");

        /// <summary>
        /// A completed task
        /// </summary>
        private static readonly Task CompletedTask = Task.FromResult(true);


        public string locale = "zh-cn";

        /// <summary>
        /// Cancellation token used to stop sending the audio.
        /// </summary>
        private readonly CancellationTokenSource cts = new CancellationTokenSource();



        private readonly Preferences preferences;

        private SpeechModel _speechModel;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="locale">lang para</param>
        public SpeechService(SpeechModel model)
        {
            _speechModel = model;
            preferences = new Preferences(_speechModel.Locale, ShortPhraseUrl, new CognitiveServicesAuthorizationProvider(_speechModel.SpeechSubKey));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="audioStream"></param>
        /// <returns></returns>
        async Task<RecognitionPhrase> ISpeechService.ReconizeAudioStreamAsync(Stream audioStream)
        {
            var t = new TaskCompletionSource<RecognitionPhrase>();
            await ReconizeAudioStream(audioStream, null, (result) => {
                var phrase = result.Phrases;
                var maxValue = phrase.Max(x => x.Confidence);
                t.SetResult(phrase.First(x => x.Confidence == maxValue));
                return Task.FromResult("");
            });
            return await t.Task;
        }

        /// <summary>
        /// get recognition from audio stream 
        /// </summary>
        /// <param name="audioStream">the audio stream</param>
        /// <param name="OnPartialResult">realtime outcome</param>
        /// <param name="OnRecognitionResult">the last outcome</param>
        /// <returns></returns>
        public async Task ReconizeAudioStream(Stream audioStream, Func<RecognitionPartialResult, Task> OnPartialResult, Func<RecognitionResult, Task> OnRecognitionResult)
        {
            using (var speechClient = new SpeechClient(preferences))
            {
                speechClient.SubscribeToPartialResult(OnPartialResult);
                speechClient.SubscribeToRecognitionResult(OnRecognitionResult);

                // create an audio content and pass it a stream.
                var deviceMetadata = new DeviceMetadata(DeviceType.Near, DeviceFamily.Desktop, NetworkType.Ethernet, OsName.Windows, "1607", "Dell", "T3600");
                var applicationMetadata = new ApplicationMetadata("SampleApp", "1.0.0");
                var requestMetadata = new RequestMetadata(Guid.NewGuid(), deviceMetadata, applicationMetadata, "SampleAppService");

                await speechClient.RecognizeAsync(new SpeechInput(audioStream, requestMetadata), this.cts.Token).ConfigureAwait(false);
                audioStream.Dispose();
            }
        }

        /// <summary>
        /// Sends a speech recognition request to the speech service
        /// </summary>
        /// <param name="audioFile">The audio file.</param>
        /// <param name="locale">The locale.</param>
        /// <param name="serviceUrl">The service URL.</param>
        /// <param name="subscriptionKey">The subscription key.</param>
        /// <returns>
        /// A task
        /// </returns>
        public async Task Excute(string audioFile, string locale, Uri serviceUrl, string subscriptionKey, Func<RecognitionPartialResult, Task> OnPartialResult, Func<RecognitionResult, Task> OnRecognitionResult)
        {
            // create the preferences object
            var preferences = new Preferences(locale, serviceUrl, new CognitiveServicesAuthorizationProvider(subscriptionKey));

            // Create a a speech client
            using (var speechClient = new SpeechClient(preferences))
            {
                speechClient.SubscribeToPartialResult(OnPartialResult);
                speechClient.SubscribeToRecognitionResult(OnRecognitionResult);

                // create an audio content and pass it a stream.
                using (var audio = new FileStream(audioFile, FileMode.Open, FileAccess.Read))
                {
                    var deviceMetadata = new DeviceMetadata(DeviceType.Near, DeviceFamily.Desktop, NetworkType.Ethernet, OsName.Windows, "1607", "Dell", "T3600");
                    var applicationMetadata = new ApplicationMetadata("SampleApp", "1.0.0");
                    var requestMetadata = new RequestMetadata(Guid.NewGuid(), deviceMetadata, applicationMetadata, "SampleAppService");

                    await speechClient.RecognizeAsync(new SpeechInput(audio, requestMetadata), this.cts.Token).ConfigureAwait(false);
                }
            }
        }


        public async Task Excute(Stream audioStream, string locale, Uri serviceUrl, string subscriptionKey, Func<RecognitionPartialResult, Task> OnPartialResult, Func<RecognitionResult, Task> OnRecognitionResult)
        {
            // create the preferences object
            var preferences = new Preferences(locale, serviceUrl, new CognitiveServicesAuthorizationProvider(subscriptionKey));

            // Create a a speech client
            using (var speechClient = new SpeechClient(preferences))
            {
                speechClient.SubscribeToPartialResult(OnPartialResult);
                speechClient.SubscribeToRecognitionResult(OnRecognitionResult);

                // create an audio content and pass it a stream.
                var deviceMetadata = new DeviceMetadata(DeviceType.Near, DeviceFamily.Desktop, NetworkType.Ethernet, OsName.Windows, "1607", "Dell", "T3600");
                var applicationMetadata = new ApplicationMetadata("SampleApp", "1.0.0");
                var requestMetadata = new RequestMetadata(Guid.NewGuid(), deviceMetadata, applicationMetadata, "SampleAppService");

                await speechClient.RecognizeAsync(new SpeechInput(audioStream, requestMetadata), this.cts.Token).ConfigureAwait(false);
                audioStream.Dispose();
            }
        }
    }
}