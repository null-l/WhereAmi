namespace WhereAmi
{

    /// <summary>
    /// Factory creating <see cref="ISpeechAction"/> from the <see cref="RecognizedPhrase" />
    /// </summary>
    public interface ISpeechActionFactory
    {
        /// <summary>
        /// Determines that action can be created from a provided phrase
        /// </summary>
        bool CanCreate( RecognizedPhrase recognizedPhrase );

        /// <summary>
        /// Creating an action from a provided phrase.
        /// Note: Ability create must be checked via <see cref="CanCreate"/> method first
        /// </summary>
        ISpeechAction Create( RecognizedPhrase recognizedPhrase );
    }

}