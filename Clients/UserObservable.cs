using BackendTests.Models.Responses.Base;

namespace BackendTests.Clients
{
    public class UserObservable : IObservable<CommonResponse<object>>
    {
        private List<IObserver<CommonResponse<object>>> observers;

        public UserObservable()
        {
            observers = new List<IObserver<CommonResponse<object>>>();
        }

        public IDisposable Subscribe(IObserver<CommonResponse<object>> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }

            return new Unsubscriber(observers, observer);
        }

        public void NotifyObservers(CommonResponse<object> response)
        {
            foreach (var observer in observers)
            {
                observer.OnNext(response);
            }
        }

        private class Unsubscriber : IDisposable
        {
            private List<IObserver<CommonResponse<object>>> _observers;
            private IObserver<CommonResponse<object>> _observer;

            public Unsubscriber(List<IObserver<CommonResponse<object>>> observers, IObserver<CommonResponse<object>> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                {
                    _observers.Remove(_observer);
                }
            }
        }
    }
}
