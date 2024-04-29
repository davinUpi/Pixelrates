using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrapper : PersistentSingletone<Bootstrapper>
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static async void Init()
    {
        Debug.Log("Bootrsapper. . . ");
        await AsyncOperationExtensions.AsTask(SceneManager.LoadSceneAsync("Base Scene", LoadSceneMode.Single));
    }
    

}
