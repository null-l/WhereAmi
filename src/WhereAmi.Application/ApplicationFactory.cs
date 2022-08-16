namespace WhereAmi
{

    public class ApplicationFactory
    {
        public static IApplication Create( string recognitionApiKey, string recognitionRegion, string locationApiToken )
        {
            ValidateArgument( recognitionApiKey, nameof(recognitionApiKey) );
            ValidateArgument( recognitionRegion, nameof(recognitionRegion) );
            ValidateArgument( locationApiToken, nameof(locationApiToken) );
            var speechRecognitionService = new SpeechRecognitionService( recognitionApiKey, recognitionRegion );
            var locationService = new LocationInformationService( locationApiToken );
            var actionProvider = new SpeechActionProvider( new ISpeechActionFactory[] { new WhereAmISpeechActionFactory( locationService ) } );
            return new Application( speechRecognitionService, actionProvider );
        }

        private static void ValidateArgument( string argumentValue, string argumentName )
        {
            if( string.IsNullOrWhiteSpace( argumentValue ) )
            {
                throw new ArgumentException( "Value cannot be null or whitespace.", argumentName );
            }
        }
    }

}