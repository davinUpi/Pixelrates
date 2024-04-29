using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StartButton : MonoBehaviour
{
    public void OnStartButtonPressed()
    {
        _ = SceneLoader.Instance.LoadSceneGroup(1);
    }
}
