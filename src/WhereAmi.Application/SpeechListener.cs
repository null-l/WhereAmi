using System.Reactive.Subjects;

namespace WhereAmi
{

    internal class SpeechListener : IObservable<RecognizedPhrase>, IDisposable
    {
        private volatile Subject<RecognizedPhrase> _subject;

        public SpeechListener( ISpeechRecognitionService recognitionService )
        {
            _subject = new Subject<RecognizedPhrase>();
            RecognizeAsync( recognitionService ).ConfigureAwait( false );
        }

#region IDisposable
        public void Dispose()
        {
            _subject.Dispose();
        }
#endregion

#region Implementation of IObservable<out RecognizedPhrase>
        public IDisposable Subscribe( IObserver<RecognizedPhrase> observer ) => _subject.Subscribe( observer );
#endregion

        private async Task RecognizeAsync( ISpeechRecognitionService recognitionService )
        {
            while (!_subject.IsDisposed)
            {
                try
                {
                    var phrase = await recognitionService.RecognizeAsync().ConfigureAwait( false );
                    if( !_subject.IsDisposed )
                    {
                        _subject.OnNext( phrase );
                    }
                }
                catch (Exception exception)
                {
                    if( !_subject.IsDisposed )
                    {
                        _subject.OnError( exception );
                    }
                }
            }
        }
    }

}