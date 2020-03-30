# RX Study Note
 Reactive Extensions Practice(RX) by using RX.net
 
## Progress

### Learning Source
* [Reactive Extensions for .NET](https://www.youtube.com/playlist?list=PLHfwoPeLRqw7XOffuusep6CJRo6Yh6IYq)- by Sathyaish Chakravarthy

### Notes
* Realised an object which is applying IObservable interface. When observable object has bees subscribed, the function will be triggered.

* The basic functions include: OnNext, OnError, and OnCompleted.

```csharp
public class Observer<T> : IObserver<T>
{
    public void OnNext(T value)
    {
        Console.WriteLine($"With value {value.ToString()}");
    }

    public void OnError(Exception error)
    {
        Console.WriteLine($"Error: {error.Message}.");
    }

    public void OnCompleted()
    {
        Console.WriteLine($"Observation done.");
    }
}
```

* The subscription can be new as standard way(input observer<T> as parameter), which is declear as:

```csharp
var subscription = observable.Subscribe(new Observer<int>());
```

or, it could delcear as lamda expression:
```csharp
var subscription = observable.Subscribe(
    Console.WriteLine, //OnNext
    (error) => { Console.WriteLine($"Error: {error.Message}"); }, //OnError
    () => { Console.WriteLine("Completed"); }); //OnCompleted
```

* Could also use Observer create method from "System.Reactive" as safe observer, so that it wont misorder the behave if there is completed or error:

```csharp
var observable = new MyRangeObservable(1, 8);

var observer = Observer.Create<int>(
    Console.WriteLine,
    ex => Console.WriteLine(ex.Message),
    () => Console.WriteLine("Done"));

var subscription = observable.Subscribe(observer);
subscription.Dispose();
```

* Try to avoid to create our own observer(IObservable interface) in order to prevent from misorder behavior. Better use the built opertor in RX.net.


## Questions


