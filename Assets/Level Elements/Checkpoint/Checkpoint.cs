using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    protected Animator _animator;
    protected EventListener _listener;

    [SerializeField] protected EventChannel DeactivateCheckpointsChannel;

    private bool active = false;

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
            Activate();
        }
    }

    protected void Activate()
    {
        if(!active) 
        {
            _listener.ignore = true;
            if (DeactivateCheckpointsChannel != null)
                DeactivateCheckpointsChannel.Invoke(new Empty());

            _animator.SetTrigger("Activate");
            GameManager.instance.SetCheckpoint(this);
            active = true;
            _listener.ignore = false;
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
