using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class RaccoonController : MonoBehaviour
{
    // private UIManager uI;
    private GameManager gameManager;
    private InputActionAsset actions;
    private InputAction move;
    private float speed = 3f;
    private bool isCrouching = false;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D rb;
    private Collider2D coll;
    private Vector3 startPosition;

    // void OnEnable()
    // {
    //     actions.FindActionMap("Raccoon").Enable();
    //     actions.FindActionMap("Raccoon").FindAction("Crouch").performed += OnCrouch;
    //     actions.FindActionMap("Raccoon").FindAction("Crouch").canceled += OnStandUp;
    // }
    // void OnDisable()
    // {
    //     actions.FindActionMap("Raccoon").Disable();
    //     actions.FindActionMap("Raccoon").FindAction("Crouch").performed -= OnCrouch;
    //     actions.FindActionMap("Raccoon").FindAction("Crouch").canceled -= OnStandUp;
    // }

    public void Initialize(GameManager gameManager, RaccoonController player, InputActionAsset actions, float playerSpeed, Vector3 position)
    {
        this.gameManager = gameManager;
        this.actions = actions;
        this.speed = playerSpeed;
        this.startPosition = position;

        PlayerInstantiate(startPosition);

        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();

        move = actions.FindActionMap("Raccoon").FindAction("Move");
        // actions.FindActionMap("Raccoon").Enable();
        // actions.FindActionMap("Raccoon").FindAction("Crouch").performed += OnCrouch;
        // actions.FindActionMap("Raccoon").FindAction("Crouch").canceled += OnStandUp;

    }
    
    private void PlayerInstantiate(Vector3 position)
    {
        transform.position = position;
    }
    
    public void Process()
    {
        Move();
    }

    private void Move()
    {
        Vector2 movement = Time.deltaTime * speed * move.ReadValue<Vector2>();
         
        spriteRenderer.flipX = movement.x < 0;

        if (isCrouching) return;

        transform.Translate( movement.x, movement.y,  0f, Space.World);

        // animator.SetFloat("speed", Math.Abs(xAxis.ReadValue<float>()));
    }

    private void OnCrouch(InputAction.CallbackContext callbackContext)
    {
        animator.SetBool("on crouch", true);
        isCrouching = true;
    }

    private void OnStandUp(InputAction.CallbackContext callbackContext)
    {
        animator.SetBool("on crouch", false);
        isCrouching = false;
    }

    // public void CaughtAnOpossum()
    // {
    //     score += 1;
    //     uI.IncreaseCounter();
    // }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Opossum"))
        {
            // CaughtAnOpossum();
        }
    }

}
