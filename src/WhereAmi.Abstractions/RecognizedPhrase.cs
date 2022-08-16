namespace WhereAmi
{

    /// <summary>
    /// Phrase recognized by the <see cref="ISpeechRecognitionService"/>.
    /// </summary>
    public class RecognizedPhrase
    {
        public RecognizedPhrase( string text )
        {
            Text = text ?? string.Empty;
        }

        /// <summary>
        /// Text from which phrase consist of.
        /// </summary>
        public virtual string Text { get; }

        public override string ToString() => Text;
    }

}