using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Animator _animator;

    [Header("Main Data SO")]
    [SerializeField] private PlayerData playerData;

    [SerializeField] private float IFrameTime = 1f;

    private bool _alive;

    private bool IFrameActive = false;

    private IEnumerator IFrameTimer()
    {
        IFrameActive = true;
        yield return new WaitForSeconds(IFrameTime);
        IFrameActive = false;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        playerData.LatestHealthValue += value => _alive  = value != 0;
    }

    public void TakeDamage(float damage)
    {
        if (!IFrameActive) 
        {
            StartCoroutine(IFrameTimer());
            if (playerData != null)
                playerData.ReduceHealth(damage);

            _animator.SetTrigger("hit");
        }
    }
    
}
