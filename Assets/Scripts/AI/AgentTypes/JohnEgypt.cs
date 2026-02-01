using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class JohnEgypt : Character
{    
    [SerializeField] Transform[] pois;
    StateMachine stateMachine;
    // States
    WanderState wanderState;
    WaitState waitState;
    RunState runState;
    AlertState alertState;
    StunState stunState;


    void Start()
    {
        stun = false;

        // Declare states
        stunState = new StunState(gameObject);
        wanderState = new WanderState(gameObject);
        waitState = new WaitState(gameObject);
        runState = new RunState(gameObject);
        alertState = new AlertState(gameObject);

        stateMachine = new StateMachine(waitState);
        
        // Declare transitions

        // FOR EVERYONE
        //stateMachine.AddAnyTransition(runState, new FuncPredicate(() => runState.observer.detectedPlayer));
        stateMachine.AddTransition(stunState, waitState, new FuncPredicate(() => stunState.hasWaitedStun));

        stateMachine.AddAnyTransition(stunState, new FuncPredicate(() => stun));
        stateMachine.AddAnyTransition(alertState, new FuncPredicate(() => alertState.observer.isAlert));

        stateMachine.AddTransition(alertState, runState, new FuncPredicate(() => alertState.observer.detectedPlayer));
        stateMachine.AddTransition(runState, alertState, new FuncPredicate(() => !runState.observer.detectedPlayer));

        stateMachine.AddTransition(alertState, waitState, new FuncPredicate(() => !alertState.observer.isAlert));

        // From wander to wait and viceversa
        stateMachine.AddTransition(wanderState, waitState, new FuncPredicate(() => wanderState.destinationReached));
        stateMachine.AddTransition(waitState, wanderState, new FuncPredicate(() => waitState.hasWaited));
    }

    void FixedUpdate()
    {
        stateMachine.FixedUpdate();
        if (stun) stun = false;
    }
}
