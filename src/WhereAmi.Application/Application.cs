namespace WhereAmi
{

    internal class Application : IApplication
    {
        private readonly ISpeechRecognitionService _speechRecognitionService;
        private readonly ISpeechActionProvider _speechActionProvider;
        private readonly object _listenerLock = new object();
        private volatile SpeechListener? _listener;
        private volatile IDisposable? _subscribtion;

        public Application( ISpeechRecognitionService speechRecognitionService, ISpeechActionProvider speechActionProvider )
        {
            _speechRecognitionService = speechRecognitionService ?? throw new ArgumentNullException( nameof(speechRecognitionService) );
            _speechActionProvider = speechActionProvider ?? throw new ArgumentNullException( nameof(speechActionProvider) );
        }

#region IDisposable
        public void Dispose()
        {
            StopListening();
        }
#endregion

        //ToDo: Handle exceptions via this event
        public event Action<Exception>? ErrorOccured;

        public void StartListening()
        {
            lock (_listenerLock)
            {
                if( _listener != null )
                {
                    StopListening();
                }
                _listener = new SpeechListener( _speechRecognitionService );
                _subscribtion = _listener.Subscribe(
                    OnNext,
                    OnError
                );
            }
        }

        public void StopListening()
        {
            lock (_listenerLock)
            {
                _listener?.Dispose();
                _subscribtion?.Dispose();
            }
        }

        private void OnError( Exception exception ) => ErrorOccured?.Invoke( exception );

        private void OnNext( RecognizedPhrase phrase )
        {
            if( _speechActionProvider.TryProvide( phrase, out var action ) )
            {
                action.ExecuteAsync();
            }
        }
    }

}