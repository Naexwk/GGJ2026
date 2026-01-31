using UnityEngine;
using UnityEngine.AI;

public class WanderState : BaseState
{
    float wanderDistance = 5f;
    float distanceThreshold = 2f;
    Vector2 destination;
    public WanderState(GameObject go, NavMeshAgent agent) : base(go, agent) {}

    //public override void 

    public override void FixedUpdate()
    {
        if (destination == null) RollDestination();
        if (Vector2.Distance(go.transform.position, destination) <= distanceThreshold) RollDestination();
    }

    public void RollDestination()
    {
        destination = (Vector2)go.transform.position + Random.insideUnitCircle * wanderDistance;
        agent.SetDestination(destination);
    }
}