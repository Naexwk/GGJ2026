using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isSus;
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Animator animator; 

    // Start is called once before the first execution of Update after the MonoBehaviour is createdz
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameManager.Instance.StartGame(); // Start the game via GameManager :D
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); /// yippie vectors :D
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontalInput, verticalInput) * moveSpeed; // Move speed applied to both axes

        rb.linearVelocity = movement; // Set the Rigidbody2D's velocity

        movement.Normalize(); // Normalize movement vector to maintain consistent speed in all directions

        if (movement == new Vector2(0,1)) // If going up
        {
            // Trigger walking animation here if needed
            animator.SetBool("Up", true);
            animator.SetBool("Idle", false);

        }
        else if (movement == new Vector2(0,-1)) // If going down
        {
            // Trigger walking animation here if needed
            animator.SetBool("Down", true);
            animator.SetBool("Idle", false);
        }
        else if (movement == new Vector2(1,0)) // If going right
        {
            // Trigger walking animation here if needed
            animator.SetBool("Right", true);
            animator.SetBool("Idle", false);
        }
        else if (movement == new Vector2(-1,0)) // If going left
        {
            // Trigger walking animation here if needed
            animator.SetBool("Left", true);
            animator.SetBool("Idle", false);
        }
        else if (movement == new Vector2(-1, 1) || movement == new Vector2(-1, -1)) // If going left diagonally
        {
            // Trigger walking animation here if needed
            animator.SetBool("Left", true);
            animator.SetBool("Idle", false);
        }
        else if (movement == new Vector2(1, 1) || movement == new Vector2(1, -1)) // If going right diagonally
        {
            // Trigger walking animation here if needed
            animator.SetBool("Right", true);
            animator.SetBool("Idle", false);
        }
        else if (movement == new Vector2(0,0))
        {
            // Trigger idle animation here if needed
            animator.SetBool("Idle", true);
            animator.SetBool("Up", false);
            animator.SetBool("Down", false);
            animator.SetBool("Right", false);
            animator.SetBool("Left", false);
        }

        Debug.Log("Player Movement Vector: " + movement);

        //No rotation in order to maintain animations consistent
        /*
        if (movement != Vector2.zero) // Only rotate if there is movement :D
        {
            rb.rotation = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg; // Rotate to face movement direction
            //
        }
        */

        if (Input.GetKeyDown(KeyCode.Space)) // Posses others with the space key :D
        {
            AudioManager.Instance.PlaySFX(1); // Play possesion sound
        }

        if (Input.GetKeyDown(KeyCode.Q)) // Logic to interact with stuff :D
        {
            //Logic to interact with stuff here
            AudioManager.Instance.PlaySFX(3); // Play interaction sound
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall")) // If we collide with a wall :D
        {
            AudioManager.Instance.PlaySFX(2); // Play wall touch sound
            Debug.Log("Player has collided with Wall!");

            GameManager.Instance.EndGame(); // End the game "TEST" 
        }
    }
}
