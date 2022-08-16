using System.Threading;
using System.Threading.Tasks;

namespace WhereAmi
{

    /// <summary>
    /// The speech action
    /// </summary>
    public interface ISpeechAction
    {
        /// <summary>
        /// Executes action asynchronously
        /// </summary>
        Task ExecuteAsync( CancellationToken cancellationToken = default );
    }

}