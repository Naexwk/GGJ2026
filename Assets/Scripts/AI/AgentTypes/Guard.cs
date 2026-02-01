using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Guard : MonoBehaviour
{    
    StateMachine stateMachine;

    // States
    WanderState wanderState;
    WaitState waitState;
    WorkState_Guard workState;

    void Start()
    {
        // Declare states
        wanderState = new WanderState(gameObject);
        waitState = new WaitState(gameObject);
        workState = new WorkState_Guard(gameObject);

        stateMachine = new StateMachine(waitState);
        
        // Declare transitions

        // From wait to work and viceversa
        stateMachine.AddTransition(waitState, workState, new FuncPredicate(() => workState.hasRested));
        stateMachine.AddTransition(workState, waitState, new FuncPredicate(() => workState.isTired));

        // From wander to wait and viceversa
        stateMachine.AddTransition(wanderState, waitState, new FuncPredicate(() => wanderState.destinationReached));
        stateMachine.AddTransition(waitState, wanderState, new FuncPredicate(() => waitState.hasWaited));
    }

    void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }
}
