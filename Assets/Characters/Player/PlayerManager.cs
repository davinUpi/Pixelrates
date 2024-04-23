using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    [SerializeField] private float IFrameTime = 1f;

    private bool IFrameActive = false;

    private IEnumerator IFrameTimer()
    {
        IFrameActive = true;
        yield return new WaitForSeconds(IFrameTime);
        IFrameActive = false;
    }

    public void TakeDamage(float damage)
    {
        if (!IFrameActive) 
        {
            StartCoroutine(IFrameTimer());
            if (playerData != null)
                playerData.ReduceHealth(damage);
        }
    }
}
