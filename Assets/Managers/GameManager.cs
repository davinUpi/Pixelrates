using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private PlayerData playerData;

    private GameObject playerObject;

    private GameObject currentCheckpoint;

    [Header("Event Channels")]
    [SerializeField] private GameObjectEventChannel PlayerSpawnedEvent;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

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

    public void SetCheckpoint(Checkpoint checkpoint)
    {
        currentCheckpoint = checkpoint.gameObject;
        Debug.Log("New Checkpoint");
    }
}
