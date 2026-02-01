using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    //CinemachineCamera cam;
    public bool isSus;
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 5f;
    private Vector2 movementInput;
    [SerializeField] private Animator animator;
    Character vesselCharacter;
    [SerializeField] private bool isMoving;
    [SerializeField] private bool isPossesing;
    [SerializeField] private bool isInteracting;

    List<GameObject> interactables;
    List<GameObject> possessables;

    GameObject possessionTarget;

    [SerializeField] GameObject vessel;
    //[SerializeField] GameObject nextVessel;
    [SerializeField] GameObject fx;
    Collider2D coll;

    void Awake()
    {
        coll = GetComponent<Collider2D>();
        //cam = GameObject.FindGameObjectWithTag("CinemachineCamera").GetComponent<CinemachineCamera>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is createdz
    void Start()
    {
        interactables = new List<GameObject>();
        possessables = new List<GameObject>();
        
        //cam.Target.TrackingTarget = vessel.transform;

        //GameManager.Instance.StartGame(); // Start the game via GameManager :D

        vesselCharacter = vessel.GetComponent<Character>();

        rb = vessel.GetComponent<Rigidbody2D>();
        animator = vessel.GetComponentInChildren<Animator>();

        vessel.GetComponent<NavMeshAgent>().enabled = false;
        vesselCharacter.enabled = false;
    }

    // Update is called once per frame
    void Update() // Handle inputs and animations in frame time
    {
        transform.position = vessel.transform.position;
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

        MaskFX();
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

    void MaskFX ()
    {
        float minDistance = float.MaxValue;
        GameObject target = null;
        foreach (var possessable in possessables)
        {
            float dist = Vector3.Distance(gameObject.transform.position, possessable.transform.position);
            if (dist < minDistance && possessable != vessel)
            {
                target = possessable;
                minDistance = dist;
            }
        }

        possessionTarget = target;

        if (possessionTarget != null)
        {
            fx.SetActive(true);
            fx.transform.position = possessionTarget.transform.position;
        } else
        {
            fx.SetActive(false);
        }

    }

    void DoPossesion() 
    {
        if (possessionTarget == null) return;
        vesselCharacter.enabled = true;
        vesselCharacter.Stun();
        vessel.GetComponent<NavMeshAgent>().enabled = true;

        vessel = possessionTarget;

        vesselCharacter = vessel.GetComponent<Character>();

        rb = vessel.GetComponent<Rigidbody2D>();
        animator = vessel.GetComponentInChildren<Animator>();

        vesselCharacter.enabled = false;
        vessel.GetComponent<NavMeshAgent>().enabled = false;

        coll.enabled = false;
        coll.enabled = true;
        
        AudioManager.Instance.PlaySFX(1); // Play possesion sound
        animator.SetTrigger("isPossesing");
    }

    void DoInteraction() 
    {
        float minDistance = float.MaxValue;
        GameObject target = null;
        foreach (var interactable in interactables)
        {
            float dist = Vector3.Distance(gameObject.transform.position, interactable.transform.position);
            if (dist < minDistance)
            {
                target = interactable;
                minDistance = dist;
            }
        }

        target?.GetComponent<IInteractable>()?.Interact();

        Debug.Log("Player has attempted to interact with an object!");
        AudioManager.Instance.PlaySFX(2); // Play interaction sound
        animator.SetTrigger("isInteracting");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entered");
        if(other.CompareTag("Character"))
        {
            Debug.Log("found character");
            possessables.Add(other.gameObject);
        } 
        else if(other.CompareTag("Interactable"))
        {
            interactables.Add(other.GetComponent<GameObject>());
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Character"))
        {
            if (possessables.Contains(other.gameObject))
            possessables.Remove(other.gameObject);
        } 
        else if(other.CompareTag("Interactable"))
        {
            if (interactables.Contains(other.gameObject))
            interactables.Remove(other.gameObject);
        }
    }
}
