using Eflatun.SceneReference;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    [SerializeField] private SceneGroup[] sceneGroups;

    bool isLoading;
    public readonly SceneGroupManager manager = new SceneGroupManager();
    public Scene BaseScene { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        BaseScene = gameObject.scene;

        manager.OnSceneLoaded += sceneName => Debug.Log($"Loaded: {sceneName}");
        manager.OnSceneUnloaded += sceneName => Debug.Log($"Unloaded: {sceneName}");
        manager.OnSceneGroupLoaded += () => Debug.Log("Scene group loaded");
    }

    async void Start()
    {
        await LoadSceneGroup(0);
    }

    private void Update()
    {
        if (!isLoading) return;
    }

    public async Task LoadSceneGroup(int index)
    {
        LoadingPorgress progress = new LoadingPorgress();
        await manager.LoadScene(sceneGroups[index], progress);
        await manager.UnloadScene();
    }

}

public class LoadingPorgress : IProgress<float>
{
    public event Action<float> Progressed;

    const float ratio = 1f;

    public void Report(float value)
    {
        Progressed?.Invoke(value / ratio);
    }
}
