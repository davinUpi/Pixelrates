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

    public Transform currentCheckpoint;

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
}
