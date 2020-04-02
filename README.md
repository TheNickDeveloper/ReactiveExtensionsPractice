# Reactive Extensions
 Reactive Extensions Practice(RX) by using RX.net
 
## RX Study Note

### Learning Source
* [Reactive Extensions for .NET](https://www.youtube.com/playlist?list=PLHfwoPeLRqw7XOffuusep6CJRo6Yh6IYq)- by Sathyaish Chakravarthy

### What is RX
In my point of view, Reactive Extensions is a way to detect the happening event, which is event driving mechanism, for doing any further action.

### Steps for RX
There are four steps for doing RX:

```csharp
* Step1: New a Observable
----------------------------------
//build-in RX.net package
IObservable observable = Observable.Range(1, 8);

//from class MyRangeObservable:IObservable<int>
MyRangeObservable observable = new MyRangeObservable(1, 8);

* Step2: New a Observer
----------------------------------
//create a observer by lambda expression
IObserver observer = Observer.Create<int>(
        Console.WriteLine,
        ex => Console.WriteLine(ex.Message),
        () => Console.WriteLine("Done"));

* Step3: Subscribe Oberver
----------------------------------
IDisposable subscription = observable.Subscribe(observer);

*Step4: Dispose it
----------------------------------
subscription.Dispose();

```

or, there is clean way like:

```csharp
//Observable create method, observer created via lambda expression
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
        // have in-buit disposable method
        return Disposable.Empty;
    });

//Obserable subscribe
var subscription = obserable.Subscribe(
    x => Console.WriteLine(x.Id),
    ex => Console.WriteLine(ex.Message),
    () => Console.WriteLine("OKOK"));
```

### Key Varable
IObservable and IObserver.

#### IObservable

* Object need to be observed. Could new a object/class for it, which need to implement interface: IObservable<T>.


```csharp
class PeopleObservable : IObservable<PersonInfo>
{
    private List<PersonInfo> _people;

    public PeopleObservable(List<PersonInfo> people)
    {
        _people = people;
    }
    
    // have to be Idisposable, otherwise cannot dispose it
    public IDisposable Subscribe(IObserver<PersonInfo> observer)
    {
        try
        {
            foreach (var person in _people)
            {
                if (person.Id == 2)
                {
                    //observer.OnCompleted();
                    throw new FormatException("Cannot be 2");
                }
                observer.OnNext(person);
            }
            return new MyDisposable();
        }
        catch (Exception ex)
        {
            observer.OnError(ex);
            return new MyDisposable();
        }
    }
}
```

* Or it could be new as Observable.Create<T> from lambda.
    
```Csharp
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
```

#### IObserver 

* Instantiated an object which is applying IObservable interface could detect three kinds of event while subscribing it.
* Suggest to use in-build Oberver like:

```csharp
var subscription = observable.Subscribe(
    Console.WriteLine, //OnNext
    (error) => { Console.WriteLine($"Error: {error.Message}"); }, //OnError
    () => { Console.WriteLine("Completed"); }); //OnCompleted
```

* For further customized, could do:

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

* Then could be use like:

```csharp
var observable = new Observer<int>();
var subscription = observable.Subscribe(observer);
```



## Questions


