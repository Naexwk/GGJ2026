using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Guard : MonoBehaviour
{    
    [SerializeField] Transform[] pois;
    StateMachine stateMachine;
    // States
    WanderState wanderState;
    WaitState waitState;
    WorkState_Guard workState;
    ConfrontState confrontState;
    AlertState alertState;
    StunState stunState;

    bool stun = false;

    void Start()
    {
        stun = false;

        // Declare states
        stunState = new StunState(gameObject);
        wanderState = new WanderState(gameObject);
        waitState = new WaitState(gameObject);
        workState = new WorkState_Guard(gameObject);
        confrontState = new ConfrontState(gameObject);
        alertState = new AlertState(gameObject);

        stateMachine = new StateMachine(waitState);
        
        // Declare transitions

        // FOR EVERYONE
        //stateMachine.AddAnyTransition(runState, new FuncPredicate(() => runState.observer.detectedPlayer));
        stateMachine.AddTransition(stunState, waitState, new FuncPredicate(() => stunState.isNoLongerStunned));

        stateMachine.AddAnyTransition(stunState, new FuncPredicate(() => stun));
        stateMachine.AddAnyTransition(alertState, new FuncPredicate(() => alertState.observer.isAlert));

        stateMachine.AddTransition(alertState, confrontState, new FuncPredicate(() => alertState.observer.detectedPlayer));
        stateMachine.AddTransition(confrontState, alertState, new FuncPredicate(() => !confrontState.observer.detectedPlayer));

        stateMachine.AddTransition(alertState, waitState, new FuncPredicate(() => !alertState.observer.isAlert));
        // From wait to work and viceversa
        stateMachine.AddTransition(waitState, workState, new FuncPredicate(() => workState.hasRested));
        stateMachine.AddTransition(workState, waitState, new FuncPredicate(() => workState.isTired));

        // From wander to wait and viceversa
        stateMachine.AddTransition(wanderState, waitState, new FuncPredicate(() => wanderState.destinationReached));
        stateMachine.AddTransition(waitState, wanderState, new FuncPredicate(() => waitState.hasWaited));

        workState.SetPOIs(pois);
    }

    void FixedUpdate()
    {
        stateMachine.FixedUpdate();
        if (stun) stun = false;
    }

    public void Stun()
    {
        stun = true;
    }
}
