using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        }
    }
}
