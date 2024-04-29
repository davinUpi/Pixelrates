using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantKillBox : MonoBehaviour
{

    protected const float MAX_DAMAGE = 999f;

    protected Collider2D _col;

    protected void Awake()
    {
        _col = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.SendMessage("TakeDamage", MAX_DAMAGE);
        }
    }
}
