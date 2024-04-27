using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static GameManager instance;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private PlayerData playerData;

    private GameObject playerObject;

    private GameObject currentCheckpoint;

    [Header("Event Channels")]
    [SerializeField] private GameObjectEventChannel PlayerSpawnedEvent;

    #region player spawning

    private const float DEFAULT_SPAWN_DELAY = 1f;

    public void SpawnPlayer(Transform spawnLocation)
    {
        Debug.Log("Attempt to spawn");
        if (playerPrefab != null)
        {
            if(playerObject != null) 
            {
                playerObject.transform.position = spawnLocation.position;
            }
            else
            {
                playerObject = Instantiate(playerPrefab, spawnLocation.position, Quaternion.identity);
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

    #endregion

    public void SetCheckpoint(Checkpoint checkpoint)
    {
        currentCheckpoint = checkpoint.gameObject;
        Debug.Log("New Checkpoint");
    }
}
