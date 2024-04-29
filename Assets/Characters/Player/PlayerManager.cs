using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Animator _animator;

    [SerializeField] private PlayerData playerData;

    [SerializeField] private float IFrameTime = 1f;

    private bool _alive;
    public bool Alive
    {
        private set 
        { 
            if(_alive != value)
            {
                _alive = value;
                if (_alive == false)
                    GameManager.Instance.DelayedSpawnPlayer();
            }
        }
        get => _alive;
    }

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

    public void UpdateAliveStatus(bool status) =>
        Alive = status;
    
}
