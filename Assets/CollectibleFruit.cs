using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleFruit : MonoBehaviour
{

    private Animator _animator;

    [SerializeField] private int score = 1;
    [SerializeField] private PlayerData playerData;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnDestroy()
    {
        _animator.SetTrigger("collected");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(playerData != null)
                playerData.AddScore(score);

            Destroy(gameObject);
        }
    }


}
