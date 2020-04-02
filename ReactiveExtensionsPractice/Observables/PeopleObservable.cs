using System;
using System.Collections.Generic;
using System.Text;

namespace ReactiveExtensionsPractice
{
    class PeopleObservable : IObservable<PersonInfo>
    {

        private readonly List<PersonInfo> _people;

        public PeopleObservable(List<PersonInfo> people)
        {
            _people = people;
        }

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

}
