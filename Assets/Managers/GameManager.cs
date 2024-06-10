
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private GameObject endPanel;

    private GameObject playerObject;

    private GameObject currentCheckpoint;


    [Header("Event Channels")]
    [SerializeField] private GameObjectEventChannel PlayerSpawnedEvent;


    private void Start()
    {
        playerData.LatestHealthValue += value => { if (value == 0) DelayedSpawnPlayer(); };
    }

    #region player spawning

    private const float DEFAULT_SPAWN_DELAY = 1f;

    public void SpawnPlayer(Transform spawnLocation)
    {
        Debug.Log("Attempt to spawn");
        if (playerPrefab != null)
        {
            if (playerObject != null)
            {
                playerObject.transform.position = spawnLocation.position;
            }
            else
            {
                playerObject = Instantiate(playerPrefab, spawnLocation.position, Quaternion.identity);
                SceneManager.MoveGameObjectToScene(playerObject, SceneLoader.Instance.BaseScene);


            }

            playerData.ResetHealth();

            if (PlayerSpawnedEvent != null)
                PlayerSpawnedEvent.Invoke(playerObject);
        }
    }

    public void SpawnPlayer()
    {
        if (currentCheckpoint != null)
            SpawnPlayer(currentCheckpoint.transform);
    }

    private IEnumerator PlayerSpawnTimer(Transform spawnLocation)
    {
        yield return new WaitForSeconds(DEFAULT_SPAWN_DELAY);
        SpawnPlayer(spawnLocation);
    }

    public void DelayedSpawnPlayer(Transform spawnLocation)
    {
        StartCoroutine(PlayerSpawnTimer(spawnLocation));
    }

    public void DelayedSpawnPlayer()
    {
        if(currentCheckpoint != null)
            DelayedSpawnPlayer(currentCheckpoint.transform);
    }

    public void DelayedSpawnPlayer(bool isAlive)
    {
        if (currentCheckpoint != null && !isAlive)
            DelayedSpawnPlayer(currentCheckpoint.transform);
    }

    #endregion

    public void SetCheckpoint(Checkpoint checkpoint)
    {
        currentCheckpoint = checkpoint.gameObject;
        Debug.Log("New Checkpoint");
    }

    public void endGame()
    {
        Time.timeScale = 0f;
        endPanel.SetActive(true);
    }
}
