using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Guard : MonoBehaviour
{    
    StateMachine stateMachine;

    // States
    WanderState wanderState;
    WaitState waitState;

    void Start()
    {
        // Declare states
        wanderState = new WanderState(gameObject);
        waitState = new WaitState(gameObject);
        stateMachine = new StateMachine(waitState);
        
        // Declare transitions
        stateMachine.AddTransition(wanderState, waitState, new FuncPredicate(() => wanderState.destinationReached));
        stateMachine.AddTransition(waitState, wanderState, new FuncPredicate(() => waitState.hasWaited));
    }

    void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }
}
