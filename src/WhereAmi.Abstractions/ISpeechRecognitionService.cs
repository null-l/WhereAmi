using System.Threading;
using System.Threading.Tasks;

namespace WhereAmi
{

    /// <summary>
    /// The speech recognition service.
    /// </summary>
    public interface ISpeechRecognitionService
    {
        /// <summary>
        /// Listening audio input and recognizing the phrases asynchronously
        /// </summary>
        /// <returns>Enumeration of the recognized phrases</returns>
        Task<RecognizedPhrase> RecognizeAsync( CancellationToken cancellationToken = default );
    }

}