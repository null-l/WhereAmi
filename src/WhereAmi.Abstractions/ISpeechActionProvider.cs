#nullable enable
using System.Diagnostics.CodeAnalysis;

namespace WhereAmi
{

    /// <summary>
    /// Factory creating <see cref="ISpeechAction"/> from the <see cref="RecognizedPhrase" />
    /// </summary>
    public interface ISpeechActionProvider
    {
        /// <summary>
        /// Trying to provide a <see cref="ISpeechAction"/> from a <see cref="RecognizedPhrase"/>
        /// </summary>
        /// <param name="recognizedPhrase">Recognized phrase to create action from</param>
        /// <param name="action">Created action</param>
        /// <returns><c>True</c> if action is created from a provided phrase</returns>
        bool TryProvide( RecognizedPhrase recognizedPhrase, [NotNullWhen( true )] out ISpeechAction? action );
    }

}