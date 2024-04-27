using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    protected static T _instance;

    public static bool HasInstance => _instance != null;
    public static T TryGetInstance() => HasInstance ? _instance : null;

    public static T Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindAnyObjectByType<T>();
                if(_instance != null) 
                {
                    var temp = new GameObject(typeof(T).Name);
                    _instance = temp.AddComponent<T>();
                }
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        InitializeSingleton();
    }

    protected virtual void InitializeSingleton()
    {
        if (!Application.isPlaying) return;

        _instance = this as T;
    }
}
