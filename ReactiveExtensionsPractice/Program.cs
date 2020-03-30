using System;
using System.Reactive;
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
            // own defined observable
            var observable = new MyRangeObservable(1, 8);

            var observer = Observer.Create<int>(
                Console.WriteLine,
                ex => Console.WriteLine(ex.Message),
                () => Console.WriteLine("Done"));

            var subscription = observable.Subscribe(observer);

            Console.WriteLine("press any key to dispose subscription.");
            Console.ReadKey();
            subscription.Dispose();
        }
    }
}
