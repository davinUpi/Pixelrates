using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
/*
 * This class represent the player's movement system
 * 
 * Kelas ini mewakilkan sistem pergerakan pemain
 */
public class PlayerMovement : MonoBehaviour
{
    // COmponents
    private Rigidbody2D _rbody;
    private Animator _animator;

    //Serialized Fields
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpHeight = 5f;

    //Privat atriutes
    private bool _onTheGround = false;

    #region Properties
    private float _horizontalMovement;
    public float HorizontalMovement
    {
        private set
        {
            if(value != _horizontalMovement)
            {
                _horizontalMovement = value;

                // when the player changes direction
                if (_horizontalMovement != 0)
                    FaceRight = _horizontalMovement > 0;

                _animator.SetFloat("xSpeed", Mathf.Abs(_horizontalMovement));
            }
        }
        get => _horizontalMovement;
    }

    private bool _faceRight = true;
    public bool FaceRight
    {
        private set
        {
            if(_faceRight != value)
            {
                _faceRight = value;
                Flip();
            }
        }
        get => _faceRight;
    }
    #endregion
    private void Awake()
    {
        _rbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        HorizontalMovement = Input.GetAxis("Horizontal");

        Jump();
    }

    private void FixedUpdate()
    {
        Move();    
    }

    private void Move()
    {
        // override the current velocity with our
        // desired velocity based on inputed movement
        _rbody.velocity = new Vector2(
                moveSpeed * HorizontalMovement, 
                _rbody.velocity.y
            );
    }

    private void Jump()
    {
        if(_onTheGround && Input.GetButtonDown("Jump"))
        {
            _rbody.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        }

    }

    private void GroundCheck()
    {
        RaycastHit2D[] hits = new RaycastHit2D[5];
        int numhits = _rbody.Cast(Vector2.down, hits,0.5f);

        // if numhits == 0, then Player is on the ground
        _onTheGround = numhits > 0;
    }

    private void Flip()
    {
        Vector3 temp = transform.localScale;
        temp.x *= -1;
        transform.localScale = temp;
    }
}
