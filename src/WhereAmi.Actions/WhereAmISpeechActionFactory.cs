using System.Diagnostics;

namespace WhereAmi
{

    public sealed class WhereAmISpeechActionFactory : ISpeechActionFactory
    {
        private readonly ILocationInformationService _locationInformationService;

        public WhereAmISpeechActionFactory( ILocationInformationService locationInformationService )
        {
            _locationInformationService = locationInformationService;
        }

#region Implementation of ISpeechActionFactory
        public bool CanCreate( RecognizedPhrase recognizedPhrase )
            => recognizedPhrase.Text.Equals
            (
                "where am i?",
                StringComparison.InvariantCultureIgnoreCase
            );

        public ISpeechAction Create( RecognizedPhrase recognizedPhrase )
        {
            Debug.Assert( CanCreate( recognizedPhrase ), "CanCreate( recognizedPhrase )" );
            return new WhereAmISpeechAction( _locationInformationService );
        }
#endregion
    }

}