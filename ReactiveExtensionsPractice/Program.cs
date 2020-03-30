using System;
using System.Reactive.Linq;
using System.Threading;

namespace ReactiveExtensionsPractice
{
    class Program
    {
        static void Main(string[] args)
        {
            

            var observable = Observable.Range(1, 8);

            //// standard way by new class
            //var subscription = observable.Subscribe(new Observer<int>());

            //// lamda expression
            //// OnNext => OnError => OnCompleted
            //var subscription = observable.Subscribe(
            //    Console.WriteLine,
            //    (error) => { Console.WriteLine($"Error: {error.Message}"); },
            //    () => { Console.WriteLine("Completed"); });

            //Console.ReadKey();
            //subscription.Dispose();

            Demo();
        }

        static void Demo()
        {
            var observable = new MyRangeObservable(1, 8);

            var observer = new Observer<int>();

            var subscription = observable.Subscribe(observer);

            Console.WriteLine("press any key to dispose subscription.");
            Console.ReadKey();
            subscription.Dispose();
        }
    }
}
