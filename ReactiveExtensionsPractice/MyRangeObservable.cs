using System;
using System.Collections.Generic;
using System.Text;

namespace ReactiveExtensionsPractice
{
    class MyRangeObservable:IObservable<int>
    {
        private readonly int _start;
        private readonly int _count;

        public MyRangeObservable(int start, int count)
        {
            _start = start;
            _count = count;
        }

        public IDisposable Subscribe(IObserver<int> observer)
        {
            try
            {
                observer.OnError(new Exception("nono!!"));
                for (int i = _start; i < _start + _count; i++)
                {
                    observer.OnCompleted();
                    observer.OnNext(i);
                }

                observer.OnCompleted();
                return new MyDisposable();
            }
            catch (Exception ex)
            {
                observer.OnError(ex);
                return new MyDisposable();
            }
        }

    }
}
