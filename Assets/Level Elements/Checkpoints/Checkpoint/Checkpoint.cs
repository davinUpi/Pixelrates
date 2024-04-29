using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    protected Animator _animator;
    protected EventListener _listener;

    [SerializeField] protected EventChannel DeactivateCheckpointsChannel;

    protected bool active = false;

    protected void ResetDeactivateTrigger()
    {
        _animator.ResetTrigger("Deactivate");
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _listener = GetComponent<EventListener>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            _listener.enabled = false;
            Activate();
            _listener.enabled = true;
        }
    }

    protected void Activate()
    {
        if(!active) 
        {
            if (DeactivateCheckpointsChannel != null)
                DeactivateCheckpointsChannel.Invoke(new Empty());

            _animator.SetTrigger("Activate");
            GameManager.Instance.SetCheckpoint(this);
            active = true;
        }
        
    }
    
    public void Deactivate()
    {
        if(active)
        {
            _animator.SetTrigger("Deactivate");
            active = false;
        }
    }
}
