using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isSus;
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 5f;
    private Vector2 movementInput;
    [SerializeField] private Animator animator;
    [SerializeField] private bool isMoving;
    [SerializeField] private bool isPossesing;
    [SerializeField] private bool isInteracting;

    // Start is called once before the first execution of Update after the MonoBehaviour is createdz
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameManager.Instance.StartGame(); // Start the game via GameManager :D
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update() // Handle inputs and animations in frame time
    {
        GetMovementInputs(); //Get player inputs each frame
        Animate(); // Handle animations for movement
                   
        if (Input.GetKeyDown(KeyCode.E)) // Posses others with the E key :D
        {
            DoPossesion();
            
        }

        if (Input.GetKeyDown(KeyCode.Q)) // Logic to interact with Q :D
        {
            DoInteraction();
        }
    }

    private void LateUpdate() // Apply movement after all Updates are done
    {
        Vector2 movement = movementInput * moveSpeed; // get the movement vector to apply to Rigidbody2D
        rb.linearVelocity = movement; // Set the Rigidbody2D's velocity
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall")) // If we collide with a wall :D
        {
            AudioManager.Instance.PlaySFX(3); // Play wall touch sound
            Debug.Log("Player has collided with Wall!");

            GameManager.Instance.EndGame(); // End the game "TEST" 
        }
    }

    void GetMovementInputs() 
    {
        float horizontalInput = Input.GetAxis("Horizontal"); /// Move input X :D
        float verticalInput = Input.GetAxis("Vertical"); // Move input Y :D

        movementInput = new Vector2(horizontalInput, verticalInput);
    }

    void Animate() 
    {
        if (movementInput.magnitude > 0.1f || movementInput.magnitude < -0.1f) 
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
            animator.SetFloat("X", 0);
            animator.SetFloat("Y", 0);
        }

        if (isMoving) 
        {
            animator.SetFloat("X", movementInput.x);
            animator.SetFloat("Y", movementInput.y);
        }

        animator.SetBool("isMoving", isMoving);
    }

    void DoPossesion() 
    {
        
        Debug.Log("Player has attempted to posses an entity!");
        AudioManager.Instance.PlaySFX(1); // Play possesion sound
        animator.SetTrigger("isPossesing");
    }

    void DoInteraction() 
    {
        
        Debug.Log("Player has attempted to interact with an object!");
        AudioManager.Instance.PlaySFX(2); // Play interaction sound
        animator.SetTrigger("isInteracting");
    }
}
