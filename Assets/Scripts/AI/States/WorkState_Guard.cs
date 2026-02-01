using System.Collections;
using UnityEngine;

public class WorkState_Guard : BaseState
{
    float distanceThreshold = 0.5f;
    float restTime = 30f;
    public bool isGettingTired = false;
    public bool isTired = false;
    public bool hasRested = true;

    // Points of interest guys, not whatever french word
    public Transform[] pois;
    Vector3 destination;
    public WorkState_Guard (GameObject go) : base(go) {}

    public override void OnEnter()
    {
        destination = Vector3.zero;
        isGettingTired = false;
        isTired = false;
        hasRested = false;
    }

    public override void FixedUpdate()
    {
        if (isTired) return;
        if (destination == Vector3.zero) GetNewDestination();
        else if (Vector2.Distance(go.transform.position, destination) < distanceThreshold)
        {
            isTired = true;
        }
    }

    public void GetNewDestination()
    {
        destination = pois[Random.Range(0, pois.Length)].position;
        agent.SetDestination(destination);
    }

    public void SetPOIs (Transform[] _pois)
    {
        pois = _pois;
    }

    public override void OnExit()
    {
        hasRested = false;
        mono.StopAllCoroutines();
        mono.StartCoroutine(recordRestTime());
    }

    IEnumerator recordRestTime ()
    {
        yield return new WaitForSeconds(restTime);
        hasRested = true;
    }
}