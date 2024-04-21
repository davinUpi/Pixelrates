using UnityEngine;
using UnityEngine.Events;

public abstract class EventListener<T> : MonoBehaviour
{
    [SerializeField] private EventChannel<T> eventChannel;
    [SerializeField] private UnityEvent<T> unityEvent;

    public bool ignore = false;

    protected void Awake()
    {
        eventChannel.Register(this);
    }

    protected void OnDestroy()
    {
        eventChannel.Deregister(this);
    }

    public void Raise(T value)
    {
        if(!ignore)
            unityEvent?.Invoke(value);
    }
}

public class EventListener : EventListener<Empty> { }
