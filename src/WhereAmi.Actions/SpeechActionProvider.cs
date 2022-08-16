using System.Diagnostics.CodeAnalysis;

namespace WhereAmi
{

    public sealed class SpeechActionProvider : ISpeechActionProvider
    {
        private readonly ISpeechActionFactory[] _factories;

#region Implementation of ISpeechActionProvider
        public SpeechActionProvider( IEnumerable<ISpeechActionFactory> factories )
        {
            _factories = factories.ToArray();
        }

        public bool TryProvide( RecognizedPhrase recognizedPhrase, [NotNullWhen( true )] out ISpeechAction? action )
        {
            foreach (var factory in _factories)
            {
                if( !factory.CanCreate( recognizedPhrase ) )
                {
                    continue;
                }
                action = factory.Create( recognizedPhrase );
                return true;
            }
            action = null;
            return false;
        }
#endregion
    }

}