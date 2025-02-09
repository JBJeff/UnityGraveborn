using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    public bool FacingLeft { get { return facingLeft; } set { facingLeft = value; } }
    public static PlayerController Instance;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;

    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;

    private bool facingLeft = false;

    private void Awake()
    {
        Instance = this;
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent < SpriteRenderer>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        AdjustPlayerFacingDirections();
        Move();
    }

    private void PlayerInput()
    {
        
        movement = playerControls.Player.Move.ReadValue<Vector2>();

        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
    }

    private void Move()
    { 
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void AdjustPlayerFacingDirections()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
       
        if (mousePos.x < playerScreenPoint.x)
        {
            mySpriteRenderer.flipX = true;
            FacingLeft = true;
        }
        else
        {
            mySpriteRenderer.flipX = false;
            FacingLeft = false;
        }
    }

}
