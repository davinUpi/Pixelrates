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
        yield return new WaitForSeconds(IFrameTime);
        IFrameActive = false;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        if(!IFrameActive) 
        {
            IFrameActive = true;
            if(playerData != null)
                playerData.ReduceHealth(damage);
            
            StartCoroutine(IFrameTimer());
            
        }
    }
}
