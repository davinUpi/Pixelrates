using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startpoint : Checkpoint
{

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void Start()
    {
        Activate();
        GameManager.instance.SpawnPlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Activate();
        }
    }
}
