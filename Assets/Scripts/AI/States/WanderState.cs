using UnityEngine;

// Rework get destination for validity
public class WanderState : BaseState
{
    float wanderDistance = 5f;
    float distanceThreshold = 0.5f;
    Vector2 destination;
    //bool isWaiting;
    public bool destinationReached = false;
    public WanderState(GameObject go) : base(go) {}

    public override void OnEnter()
    {
        RollDestination();
    }

    public override void FixedUpdate()
    {
        //if (destination == null) RollDestination();
        //else if (Vector2.Distance(go.transform.position, destination) <= distanceThreshold) destinationReached = true;
        if (Vector2.Distance(go.transform.position, destination) <= distanceThreshold) destinationReached = true;
    }

    public void RollDestination()
    {
        //if (isWaiting) return;
        //mono.StartCoroutine(WaitForNewDestination());
        destination = (Vector2)go.transform.position + Random.insideUnitCircle * wanderDistance;
        agent.SetDestination(destination);
    }

    /*IEnumerator WaitForNewDestination ()
    {
        isWaiting = true;
        destination = (Vector2)go.transform.position + Random.insideUnitCircle * wanderDistance;
        agent.SetDestination(destination);
        yield return new WaitForSeconds(waitTimeBetweenDestinations);
        isWaiting = false;

        /*NavMeshPath path = new NavMeshPath();
        if (agent.CalculatePath(destination, path) && path.status == NavMeshPathStatus.PathComplete)
        {
            agent.SetDestination(destination);
            yield return new WaitForSeconds(waitTimeBetweenDestinations);
        } else
        {
            Debug.DrawRay(go.transform.position, destination.normalized * Vector2.Distance(go.transform.position, destination), Color.purple);
            Debug.Break();
            Debug.Log("just put the fries in the bag dawg");
        }
    }*/

    public override void OnExit()
    {
        destinationReached = false;
    }
}