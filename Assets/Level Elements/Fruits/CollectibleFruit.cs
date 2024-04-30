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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(playerData != null)
                playerData.AddScore(score);

            _animator.SetTrigger("collected");
        }
    }

    private void DestorySelf() => Destroy(gameObject);

    public void EnableSelf() => gameObject.SetActive(true);
    private void DisableSelf() => gameObject.SetActive(false);


}
