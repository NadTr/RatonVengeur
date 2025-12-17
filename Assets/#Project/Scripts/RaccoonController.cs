using System;
// using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class RaccoonController : MonoBehaviour
{
    private const string ACTION_MAP = "Raccoon";
    private const string ACTION_MOVE = "Move";
    private const string ACTION_GRUMBLE = "Grumble";
    private const string ACTION_INTERACT = "Interact";
     private GameManager gameManager;
    private InputActionAsset actions;
    private InputAction move;
    private float speed = 3f;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D rb;
    private Collider2D coll;
    private Vector3 startPosition;
    private Vector3 frontDirection;
    // private AudioSource raccoonGrumble;
    private AudioSource footsteps;
    private float footStepDelay = 0.3f;
    private float timer = 0f;

    public void Initialize(GameManager gameManager, RaccoonController player, InputActionAsset actions, float playerSpeed, Vector3 position,AudioSource footsteps)
    {
        this.gameManager = gameManager;
        this.actions = actions;
        this.speed = playerSpeed;
        this.startPosition = position;
        // this.raccoonGrumble = raccoonGrumble;
        this.footsteps = footsteps;

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
        actions.FindActionMap(ACTION_MAP).FindAction(ACTION_GRUMBLE).performed += OnGrumble;
        actions.FindActionMap(ACTION_MAP).FindAction(ACTION_INTERACT).performed += OnInteract;
    }


    void OnDisable()
    {
        actions.FindActionMap(ACTION_MAP).Disable();
        actions.FindActionMap(ACTION_MAP).FindAction(ACTION_GRUMBLE).performed -= OnGrumble;
        actions.FindActionMap(ACTION_MAP).FindAction(ACTION_INTERACT).performed -= OnInteract;
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
        transform.Translate(movement.x, movement.y, 0f, Space.World);
        if(frontDirection.x != 0)
        {
            animator.SetBool("is forward", true);
            animator.SetBool("is up", false);
            animator.SetBool("is down", false);
            spriteRenderer.flipX = frontDirection.x == -1f;
            // Debug.Log($"flipX? {frontDirection.x == -1f}");
            
        }
        else
        {
            if (frontDirection.y == 1f)
            {                
                animator.SetBool("is up", true);
                animator.SetBool("is forward", false);
                animator.SetBool("is down", false);
            }
            else
            {
                animator.SetBool("is down", true);
                animator.SetBool("is up", false);
                animator.SetBool("is forward", false);               
            }
        }
        if (movement.sqrMagnitude > 0)
        {
            timer += Time.deltaTime;
            // Debug.Log(timer);
            if (timer >= footStepDelay)
            {
                footsteps.Play();
                timer = 0f;
            }

            animator.SetBool("is walking", true);
            // Debug.Log("walking aniomator?");

        }
        else
        {
            timer = 0f;
            animator.SetBool("is walking", false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Opossum"))
        {
            // CaughtAnOpossum();
        }

        if (collision.CompareTag("Out"))
        {
            // Debug.Log("try to get out");
            SceneManager.LoadScene("EndMenu");

        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("WaterFall"))
        {
            gameManager.WaterFallSound(this.transform, false);
        }
    }
    
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("WaterFall"))
        {
            gameManager.WaterFallSound(this.transform, true);        
        } 
    }


    private void OnGrumble(InputAction.CallbackContext callbackContext)
    {
        // Debug.Log("grmbllll");
        //make an animation
        gameManager.RaccoonGrumble(this.transform);
    }
    private void OnInteract(InputAction.CallbackContext callbackContext)
    {
        // Vector3 origin = transform.position + frontDirection.y * 0.4f * Vector3.up + frontDirection.x * 0.5f * Vector3.right;
        Vector3 origin = transform.position;
        RaycastHit2D sideHit = Physics2D.Raycast(origin, frontDirection, 0.5f);
        // Debug.DrawRay(origin, frontDirection * 10, Color.red);
        if (sideHit.collider != null)
        {
            Debug.Log($"intercact with {sideHit.collider.name}");
            if (sideHit.collider.CompareTag("Lucy"))
            {
                gameManager.LucyDialogue();
            }
            if (sideHit.collider.CompareTag("HidingPlace"))
            {
                gameManager.IsThereAnOpossumThere(sideHit.collider.gameObject);
            }
            if (sideHit.collider.CompareTag("Opossum"))
            {
                gameManager.CatchOpossum();
            }
        }
    }


}
