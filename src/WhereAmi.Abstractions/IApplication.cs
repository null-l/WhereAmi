using System;

namespace WhereAmi
{

    public interface IApplication : IDisposable
    {
        event Action<Exception> ErrorOccured;
        void StartListening();
        void StopListening();
    }

}