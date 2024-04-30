using System;
using System.Collections.Generic;
using System.Linq;
using Eflatun.SceneReference;
using UnityEngine.SceneManagement;

public enum SceneType { ActiveScene, MainMenu, UI, Level };

[Serializable]
public class SceneGroup
{

    public string GroupName = "New Scene Group";
    public List<SceneData> Scenes;

    public string FindSceneNameByType(SceneType type) =>
        Scenes.FirstOrDefault(scene => scene.type == type)?.reference.Name;

    public bool FindSceneByName(string name) =>
        Scenes.FirstOrDefault(Scene => Scene.name == name) != null;
}

[Serializable]
public class SceneData
{
    public SceneReference reference;
    public string name => reference.Name;
    public SceneType type;
}


