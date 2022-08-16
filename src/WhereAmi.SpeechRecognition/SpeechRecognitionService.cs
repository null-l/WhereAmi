using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

namespace WhereAmi
{

    public sealed class SpeechRecognitionService : ISpeechRecognitionService
    {
        private readonly SpeechConfig _speechConfig;

        public SpeechRecognitionService( string speechApiToken, string region )
        {
            if( speechApiToken == null )
            {
                throw new ArgumentNullException( nameof(speechApiToken) );
            }
            _speechConfig = SpeechConfig.FromSubscription( speechApiToken, region );
        }

        public async Task<RecognizedPhrase> RecognizeAsync( CancellationToken cancellationToken = default )
        {
            //ToDo: Configurable audio input.
            using (var audioConfig = AudioConfig.FromDefaultMicrophoneInput())
            using (var recognizer = new SpeechRecognizer( _speechConfig, audioConfig ))
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var recognitionResult = await recognizer.RecognizeOnceAsync();
                    cancellationToken.ThrowIfCancellationRequested();
                    switch (recognitionResult.Reason)
                    {
                        case ResultReason.RecognizedSpeech:
                            return new RecognizedPhrase( recognitionResult.Text );
                        case ResultReason.Canceled:
                            throw new TaskCanceledException( "Speech recognition has been canceled by the server." );
                        default:
                            // Continue recognition in other cases
                            continue;
                    }
                }
                throw new TaskCanceledException();
            }
        }
    }

}