using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private EventChannel gameFinishEventChannel;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            animator.SetTrigger("triggered");
    }

    private void finish()
    {
        if (gameFinishEventChannel != null)
            gameFinishEventChannel.Invoke(new Empty());
    }
}
