using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameManager.Instance.StartGame(); // Start the game via GameManager :D
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); /// yippie vectors :D
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontalInput, verticalInput) * moveSpeed; // Move speed applied to both axes

        rb.linearVelocity = movement; // Set the Rigidbody2D's velocity

        if (movement != Vector2.zero) // Only rotate if there is movement :D
        {
            rb.rotation = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg; // Rotate to face movement direction
            //
        }

        if (Input.GetKeyDown(KeyCode.Space)) // Posses others with the space key :D
        {
            AudioManager.Instance.PlaySFX(1); // Play possesion sound
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
