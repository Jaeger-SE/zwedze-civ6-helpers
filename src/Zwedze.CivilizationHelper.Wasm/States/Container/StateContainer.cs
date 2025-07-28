using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Zwedze.CivilizationHelper.Wasm.States.Container;

public class StateContainer<T>
{
    private readonly BehaviorSubject<T> _stateSubject;

    public StateContainer(T initialState)
    {
        _stateSubject = new BehaviorSubject<T>(initialState);
    }

    public IObservable<T> State => _stateSubject.AsObservable();

    public void Update(Func<T, T> updateFn)
    {
        var updated = updateFn(_stateSubject.Value);
        _stateSubject.OnNext(updated);
    }
}
