using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public abstract class EventChannel<T> : ScriptableObject 
{
    readonly HashSet<EventListener<T>> observers = new();

    public void Invoke(T value)
    {
        foreach (var obersever in  observers) 
        {
            obersever.Raise(value);
        }
    }

    public void Register(EventListener<T> observer) =>
        observers.Add(observer);

    public void Deregister(EventListener<T> observer) =>
        observers.Remove(observer);
    
}

public readonly struct Empty { }

[CreateAssetMenu(menuName ="Events/EventChannel")]
public class EventChannel : EventChannel<Empty> { }
