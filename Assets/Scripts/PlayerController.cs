using UnityEngine;
using UnityEngine.InputSystem;

// Force anything with this script to require a Rigidbody2D component.
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Fields")]
    [Tooltip("The movement speed of the player in meters per second.")]
    public int moveSpeed;
    [Tooltip("Vector that will be used to store keyboard movement input.")]
    public Vector2 moveInput;

    [Header("Interact Fields")]
    [Header("Whether the character is trying to interact with something or not.")]
    public bool interactInput;

    [Header("References")]
    [Tooltip("The Rigidbody2D component on this character.")]
    public Rigidbody2D rb;
    
    private void Start()
    {
        // Store a reference to the Rigidbody2D component on this object in rb.
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Check to see if the player is trying to interact.
        if (interactInput)
        {
            // Reset the boolean.
            interactInput = false;
            // Attempt an interaction with something.
            TryInteract();
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
        Debug.Log("Pressed interact button");
    }
}
