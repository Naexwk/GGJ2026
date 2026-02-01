using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    protected bool stun = false;

    void Awake()
    {
        GetComponent<NavMeshAgent>().updateRotation = false;
    }

    public void Stun()
    {
        stun = true;
    }


}