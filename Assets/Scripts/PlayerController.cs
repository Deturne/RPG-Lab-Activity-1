using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

// Force anything with this script to require a Rigidbody2D component.
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour, ISomething, ISomethingB
{
    [Header("Movement Fields")]
    [Tooltip("The movement speed of the player in meters per second.")]
    public int moveSpeed;
    [Tooltip("Vector that will be used to store keyboard movement input.")]
    public Vector2 moveInput;
    public Vector2 facingDir;
    public Vector2 lastPos;

    [Header("Interact Fields")]
    [Header("Whether the character is trying to interact with something or not.")]
    public bool interactInput;
    public float raycastLength = 3;
    public LayerMask interactableLayerMask;

    [Header("References")]
    [Tooltip("The Rigidbody2D component on this character.")]
    public Rigidbody2D rb;

    [Header("Stats")]
    public static int health = 10;
    public int level = 1;
    public static int experience = 0;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        // Store a reference to the Rigidbody2D component on this object in rb.
        rb = GetComponent<Rigidbody2D>();
        if(lastPos != null)
        {
            transform.position = lastPos;
        }
    }

    private void Update()
    {
        if(moveInput.magnitude != 0.0f)
        {
            facingDir = moveInput.normalized;
        }

        // Check to see if the player is trying to interact.
        if (interactInput)
        {
            // Reset the boolean.
            interactInput = false;
            // Attempt an interaction with something.
            TryInteract();
        }

        if(experience >= 10)
        {
            level++;
            experience -= 10; 
            health += 5;
            Debug.Log($"Level Up! New Level: {level}, Health: {health}");
        }
    }

    private void FixedUpdate()
    {
        //New velocity code for Unity 6
        // Set direction of the player's movement to match the input.
        // Then set the speed in that direction to the moveSpeed.
        rb.linearVelocity = moveInput.normalized * moveSpeed;
    }

    //InputActions parameters
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    //InputActions parameters needs to be held down
    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            interactInput = true;
        }
    }

    // Testing if interacting works.
    private void TryInteract()
    {
        Debug.DrawRay(transform.position, facingDir * raycastLength, Color.red, 1.0f);
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, facingDir, raycastLength, interactableLayerMask);

        if (!hit.collider.gameObject.CompareTag("Enemy"))
        {
            Debug.Log(hit.collider.gameObject.name);

            IConversable conversable = hit.collider.gameObject.GetComponent<IConversable>();
            conversable?.StartConversation();
        }
        else
        {
            Debug.Log("Miissed");
        }

        if (hit.collider.gameObject.CompareTag("Enemy"))
        {
            lastPos = transform.position;
            Debug.Log("Enemy detected, starting combat scene.");
            SceneManager.LoadScene("CombatScene", LoadSceneMode.Single);
        }
    }
}
