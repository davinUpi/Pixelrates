using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/*
 * This class represent the player's movement system
 * 
 * Kelas ini mewakilkan sistem pergerakan pemain
 */
public class PlayerMovement : MonoBehaviour
{

    // COmponents2
    private Rigidbody2D _rbody;
    private Animator _animator;

    // Main data source
    [Header("Main Data SO")]
    [SerializeField] private PlayerData playerData;

    //Serialized Fields
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpHeight = 5f;

    [SerializeField] private ContactFilter2D groundFilter;

    private bool _alive = true;

    //Privat atriutes

    #region Properties
    private bool _grounded = false;
    public bool Grounded
    {
        private set
        {
            if(_grounded != value)
            {
                _grounded = value;
                _animator.SetBool("grounded", _grounded);
            }
        }
        get => _grounded;
    }

    // Represent player's horizontal movement based by Input
    //
    // Mewakilkan pergerakan horizontal pmeain berdasarkan input
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

    // Represent the player's vertical speed as is
    //
    // Mewakilkan kecepatan vertikal pemain seadanya
    private float _verticalDelta;
    public float VerticalDelta
    {
        private set
        {
            if(_verticalDelta != value)
            {
                _verticalDelta = value;
                _animator.SetFloat("yDelta", _verticalDelta);
            }
        }
        get => _verticalDelta;
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
    private void Start()
    {
        playerData.LatestHealthValue += value => _alive = value != 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(_alive)
        {
            GroundCheck();
            VerticalDelta = _rbody.velocity.y;

            HorizontalMovement = Input.GetAxis("Horizontal");

            Jump();
        }
        
    }

    private void FixedUpdate()
    {
        if(_alive)
        {
            Move();
        }
    }

    private void Move()
    {
        float acceleration = HorizontalMovement * moveSpeed;
        float targetVelocity = Mathf.Clamp(_rbody.velocity.x + acceleration, -moveSpeed, moveSpeed);

        if(ShouldChangeVelocity(targetVelocity))
        {
            _rbody.velocity = new Vector2(targetVelocity, _rbody.velocity.y);
        }
    }

    private bool ShouldChangeVelocity(float targetVelocity)
    {
        float currentVelocity = _rbody.velocity.x;
        if (currentVelocity == 0) return true;

        if(targetVelocity != 0)
        {
            return Mathf.Abs(currentVelocity) < moveSpeed;
        }

        return false;
    }

    private void Jump()
    {
        if(Grounded && Input.GetButtonDown("Jump"))
        {
            _animator.SetTrigger("jumpTrigger");
            _rbody.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        }

    }

    private void GroundCheck()
    {
        RaycastHit2D[] hits = new RaycastHit2D[5];
        int numhits = _rbody.Cast(Vector2.down, groundFilter,hits,0.5f);

        // if numhits == 0, then Player is on the ground
        Grounded = numhits > 0;
    }

    private void Flip()
    {
        Vector3 temp = transform.localScale;
        temp.x *= -1;
        transform.localScale = temp;
    }
}
