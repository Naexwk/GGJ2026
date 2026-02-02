using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    protected bool stun = false;
    protected NavMeshAgent charAgent;
    protected Animator charAnimator;
    bool isMoving;

    void Awake()
    {
        charAgent = GetComponent<NavMeshAgent>();
        charAnimator = GetComponentInChildren<Animator>();
        charAgent.updateRotation = false;
    }

    public virtual void Update()
    {
        transform.rotation = Quaternion.identity;
        Animate();
    }

    public void Stun()
    {
        stun = true;
    }

    void Animate() 
    {
        Vector3 movementInput = charAgent.velocity;
        if (movementInput.magnitude > 0.1f || movementInput.magnitude < -0.1f) 
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
            charAnimator.SetFloat("X", 0);
            charAnimator.SetFloat("Y", 0);
        }

        if (isMoving) 
        {
            charAnimator.SetFloat("X", movementInput.x);
            charAnimator.SetFloat("Y", movementInput.y);
        }

        charAnimator.SetBool("isMoving", isMoving);
    }


}