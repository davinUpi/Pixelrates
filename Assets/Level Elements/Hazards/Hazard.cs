using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : InstantKillBox
{
    [SerializeField] private float damage = 1f;
    [SerializeField] private bool instantKill = false;
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.collider.SendMessage("TakeDamage", instantKill ? MAX_DAMAGE : damage);
        }
    }
}
