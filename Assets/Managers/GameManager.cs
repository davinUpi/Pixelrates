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

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void SpawnPlayer(Transform spawnLocation)
    {
        if (!playerPrefab)
        {
            if(!playerObject) 
            { 
                playerObject = Instantiate(playerPrefab, spawnLocation);
            }
            else
            {
                playerObject.transform.position = spawnLocation.position;
            }

            playerData.ResetHealth();
        }
    }

    public void SpawnPlayer()
    {
        if (!currentCheckpoint)
            SpawnPlayer(currentCheckpoint.transform);
    }

    public void SetCheckpoint(Checkpoint checkpoint)
    {
        currentCheckpoint = checkpoint.gameObject;
        Debug.Log("New Checkpoint");
    }
}
