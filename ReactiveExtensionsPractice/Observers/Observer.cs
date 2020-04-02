using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ReactiveExtensionsPractice
{
    public class Observer<T> : IObserver<T>
    {
        private readonly string _name;

        public Observer(string name = "sub1")
        {
            _name = name;
        }

        public void OnNext(T value)
        {
            Console.WriteLine($"name: {_name}, with value {value.ToString()}");
        }
        

        public void OnError(Exception error)
        {

            Console.WriteLine($"Error: {error.Message} on name {_name}");
        }

        public void OnCompleted()
        {

            Console.WriteLine($"Observation done by name {_name}.");
        }
    }
}
