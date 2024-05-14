using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Platform : MonoBehaviour
{
    private TilemapCollider2D _col;

    private bool _onThePlatform = false;
    private readonly WaitForSeconds _reactivateTime = new(0.5f);

    private IEnumerator ReactivateCollider()
    {
        yield return _reactivateTime;
        _col.enabled = true;
    }

    private void Awake()
    {
        _col = GetComponent<TilemapCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_onThePlatform && Input.GetAxisRaw("Vertical") < 0)
        {
            _col.enabled = false;
            StartCoroutine(ReactivateCollider());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            _onThePlatform = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            _onThePlatform = false;
    }
}
