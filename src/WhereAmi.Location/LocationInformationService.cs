using Newtonsoft.Json.Linq;

namespace WhereAmi
{

    public sealed class LocationInformationService : ILocationInformationService
    {
        private readonly string _ipBaseAuthenticationToken;

        public LocationInformationService( string ipBaseAuthenticationToken )
        {
            _ipBaseAuthenticationToken = ipBaseAuthenticationToken;
        }

        public async Task<string> GetLocationCountryNameAsync( CancellationToken cancellationToken = default )
        {
            string locationDataString;
            using (var httpClient = new HttpClient())
            {
                var ip = await httpClient
                    .GetStringAsync(
                        "http://myexternalip.com/raw",
                        cancellationToken
                    )
                    .ConfigureAwait( false );
                locationDataString = await httpClient
                    .GetStringAsync(
                        $"https://api.ipbase.com/v2/info?apikey={_ipBaseAuthenticationToken}&ip={ip}",
                        cancellationToken
                    )
                    .ConfigureAwait( false );
            }
            var locationData = JObject.Parse( locationDataString );
            var countryName = locationData["data"]?["location"]?["country"]?["name_translated"]?.Value<string>();
            return countryName ?? throw new NullReferenceException( nameof(countryName) );
        }
    }

}