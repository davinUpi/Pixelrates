using UnityEngine;
using UnityEngine.Events;

public abstract class EventListener<T> : MonoBehaviour
{
    [SerializeField] private EventChannel<T> eventChannel;
    [SerializeField] private UnityEvent<T> unityEvent;

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
        if(enabled)
            unityEvent?.Invoke(value);
    }
}

public class EventListener : EventListener<Empty> { }
