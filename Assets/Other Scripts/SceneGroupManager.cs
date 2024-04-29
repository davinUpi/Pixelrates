using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Eflatun.SceneReference;
using System;
using System.Linq;
using System.Threading.Tasks;

public class SceneGroupManager
{
    public event Action<string> OnSceneLoaded = delegate { };
    public event Action<string> OnSceneUnloaded = delegate { };
    public event Action OnSceneGroupLoaded = delegate { };

    public SceneGroup ActiveSceneGroup {  get; private set; }

    public async Task LoadScene(SceneGroup group, IProgress<float> progress, bool reloadDupeScene = false)
    {
        ActiveSceneGroup = group;
        var loadedScenes = new List<string>();

        await UnloadScene();

        int sceneCount = SceneManager.sceneCount;
        for (var i = 0; i < sceneCount; i++)
        {
            loadedScenes.Add(SceneManager.GetSceneAt(i).name);
        }

        var totalScenesToLoad = ActiveSceneGroup.Scenes.Count;

        var operationGroup = new AsyncOperationGroup(totalScenesToLoad);

        for(var i = 0; i < totalScenesToLoad; i++)
        {
            var sceneData = group.Scenes[i];
            if (reloadDupeScene == false && loadedScenes.Contains(sceneData.name)) continue;

            var operation = SceneManager.LoadSceneAsync(sceneData.reference.Path, LoadSceneMode.Additive);
            operationGroup.operations.Add(operation);

            OnSceneLoaded.Invoke(sceneData.name);
        }

        while (!operationGroup.IsDone)
        {
            progress?.Report(operationGroup.progress);
            await Task.Delay(100);
        }
            

        Scene ActiveScene = SceneManager.GetSceneByName(ActiveSceneGroup.FindSceneNameByType(SceneType.ActiveScene));
        Debug.Log("Active Scene: " + ActiveScene.name);
        if(ActiveScene.IsValid()) SceneManager.SetActiveScene(ActiveScene);

        OnSceneGroupLoaded.Invoke();
        

    }

    public async Task UnloadScene()
    {
        var scenes = new List<string>();
        var activeScene = SceneManager.GetActiveScene().name;

        int sceneCount = SceneManager.sceneCount;
        for(var i = sceneCount - 1; i > 0; i--)
        {
            var sceneAt = SceneManager.GetSceneAt(i);
            if (!sceneAt.isLoaded) continue;

            var sceneName = sceneAt.name;
            if(sceneName.Equals(activeScene) || sceneName.Equals("Base Scene")) continue;

            scenes.Add(sceneName);
        }

        var operationGroup = new AsyncOperationGroup(scenes.Count);
        foreach(var scene in scenes)
        {
            var operation = SceneManager.UnloadSceneAsync(scene);
            if (operation == null) continue;

            operationGroup.operations.Add(operation);

            while (!operationGroup.IsDone)
                await Task.Delay(100);

            OnSceneUnloaded.Invoke(scene);  
        }
    }

}

public readonly struct AsyncOperationGroup
{
    public readonly List<AsyncOperation> operations;

    public float progress => operations.Count == 0? 0 :operations.Average(o => o.progress);
    public bool IsDone => operations.All(o => o.isDone);

    public AsyncOperationGroup(int initialSize)
    {
        operations = new List<AsyncOperation>(initialSize);
    }
}
