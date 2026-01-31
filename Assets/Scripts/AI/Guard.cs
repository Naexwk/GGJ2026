using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Guard : MonoBehaviour
{
    StateMachine stateMachine;
    WanderState wanderState;
    NavMeshAgent agent;

    void Awake()
    {
        stateMachine = new StateMachine();
        agent = GetComponent<NavMeshAgent>();
        wanderState = new WanderState(gameObject, agent);
    }
}
