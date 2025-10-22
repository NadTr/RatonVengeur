using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class RaccoonController : MonoBehaviour
{
    private const string ACTION_MAP = "Raccoon";
    private const string ACTION_MOVE = "Move";
    private const string PAUSE = "Pause";
    private const string ACTION_CROUCH = "Crouch";
    private const string ACTION_INTERACT = "Interact";

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
    private Vector3 frontDirection;

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

        move = actions.FindActionMap(ACTION_MAP).FindAction(ACTION_MOVE);
        frontDirection = new();
        frontDirection.z = transform.position.z;

    }

    void OnEnable()
    {
        actions.FindActionMap(ACTION_MAP).Enable();
        actions.FindActionMap(ACTION_MAP).FindAction(PAUSE).performed += OnPause;
        actions.FindActionMap(ACTION_MAP).FindAction(ACTION_INTERACT).performed += OnInteract;
        actions.FindActionMap(ACTION_MAP).FindAction(ACTION_CROUCH).performed += OnCrouch;
        actions.FindActionMap(ACTION_MAP).FindAction(ACTION_CROUCH).canceled += OnStandUp;

    }


    void OnDisable()
    {
        actions.FindActionMap(ACTION_MAP).Disable();
        actions.FindActionMap(ACTION_MAP).FindAction(PAUSE).performed -= OnPause;
        actions.FindActionMap(ACTION_MAP).FindAction(ACTION_INTERACT).performed -= OnInteract;
        actions.FindActionMap(ACTION_MAP).FindAction(ACTION_CROUCH).performed -= OnCrouch;
        actions.FindActionMap(ACTION_MAP).FindAction(ACTION_CROUCH).canceled -= OnStandUp;
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
        // frontDirection.x = movement.x > 0f ? 1f : (movement.x < 0f ? -1f : 0f);
        frontDirection.x = movement.x > 0f ? 1f : (movement.x < 0f ? -1f : (movement.y != 0f ? 0f : frontDirection.x));
        frontDirection.y = movement.y > 0f ? 1f : (movement.y < 0f ? -1f :(movement.x != 0f ? 0f: frontDirection.y));
        // frontDirection.x = movement.x > 0f ? 1f : -1f ;
        // frontDirection.y = movement.y > 0f ? 1f : -1f;
        // Debug.Log(frontDirection);
        transform.Translate( movement.x, movement.y,  0f, Space.World);

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
        if (collision.CompareTag("Out"))
        {
            Debug.Log("try to get out");
        }
    }
    private void OnPause(InputAction.CallbackContext callbackContext)
    {
        gameManager.Pause();
    }
    private void OnInteract(InputAction.CallbackContext callbackContext)
    {
        Debug.Log($"front : {frontDirection}");
        Vector3 origin = transform.position + frontDirection.y * 0.5f * Vector3.up + frontDirection.x * 0.5f * Vector3.right;
        // Debug.Log(origin);
        RaycastHit2D sideHit = Physics2D.Raycast(origin, frontDirection, 0.2f);
        Debug.DrawRay(origin, frontDirection * 10, Color.red);
        if (sideHit.collider != null)
        {
            Debug.Log($"intercact with {sideHit.collider.name}");
            gameManager.IsThereAnOpossumThere(sideHit.collider.gameObject);
        }
    }

}
