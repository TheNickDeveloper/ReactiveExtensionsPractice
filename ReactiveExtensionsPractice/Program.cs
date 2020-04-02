using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;

namespace ReactiveExtensionsPractice
{
    class Program
    {
        static void Main(string[] args)
        {
            //UseInbuildMethodDemo();
            //UseIntRangeForDemo();
            //UsePersonListAsObservableDemo();
            ObserableCreate();
        }

        static void UseInbuildMethodDemo()
        {
            var observable = Observable.Range(1, 8);

            //// standard way by new class
            //var subscription = observable.Subscribe(new Observer<int>());

            // lamda expression
            // OnNext => OnError => OnCompleted
            var subscription = observable.Subscribe(
                Console.WriteLine,
                (error) => { Console.WriteLine($"Error: {error.Message}"); },
                () => { Console.WriteLine("Completed"); });

            Console.ReadKey();
            subscription.Dispose();
        }

        static void UseIntRangeForDemo()
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

        static void UsePersonListAsObservableDemo()
        {
            var people = new List<PersonInfo>
            {
                new PersonInfo { Id = 1, Name = "1"},
                new PersonInfo { Id = 2, Name = "2"},
                new PersonInfo { Id = 3, Name = "3"}
            };

            var peopleObervalbe = new PeopleObservable(people);

            var peopleObserver = Observer.Create<PersonInfo>(
                x => Console.WriteLine(x.Name),
                ex => Console.WriteLine(ex.Message),
                () => Console.WriteLine("Done"));

            var subscriptionPeople = peopleObervalbe.Subscribe(peopleObserver);

            Console.WriteLine("press any key to dispose subscription.");
            Console.ReadKey();
            subscriptionPeople.Dispose();
        }

        static void ObserableCreate()
        {
            var people = new List<PersonInfo>
            {
                new PersonInfo { Id = 1, Name = "1"},
                new PersonInfo { Id = 2, Name = "2"},
                new PersonInfo { Id = 3, Name = "3"}
            };

            var obserable = Observable.Create<PersonInfo>(x =>
            {
                foreach (var p in people)
                {
                    if (p.Id == 3)
                    {
                        x.OnCompleted();
                    }
                    x.OnNext(p);
                }
                return Disposable.Empty;
            });

            var subscription = obserable.Subscribe(
                x => Console.WriteLine(x.Id),
                ex => Console.WriteLine(ex.Message),
                () => Console.WriteLine("OKOK"));
        }
    }
}
