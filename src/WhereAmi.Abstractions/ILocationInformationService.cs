using System.Threading;
using System.Threading.Tasks;

namespace WhereAmi
{

    /// <summary>
    /// Location information service
    /// </summary>
    public interface ILocationInformationService
    {
        /// <summary>
        /// Determines current location countr name using current external IP address. 
        /// </summary>
        /// <returns>Current location country name</returns>
        Task<string> GetLocationCountryNameAsync( CancellationToken cancellationToken = default );
    }

}