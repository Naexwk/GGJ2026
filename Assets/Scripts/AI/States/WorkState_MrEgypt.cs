using System.Collections;
using UnityEngine;

public class WorkState_MrEgypt : BaseState
{
    float distanceThreshold = 0.5f;
    float restTime = 15f;
    float workTime = 1f;
    float interactTime = 30f;
    public bool isGettingTired = false;
    public bool isTired = false;
    public bool hasRested = true;
    bool isInteracting = false;

    // Points of interest guys, not whatever french word
    public Transform[] pois;
    Vector3 destination;
    public WorkState_MrEgypt (GameObject go) : base(go) {}

    public override void OnEnter()
    {
        destination = Vector3.zero;
        isGettingTired = false;
        isTired = false;
        hasRested = false;
        isInteracting = false;
        mono.StartCoroutine(recordWorkTime());
    }

    public override void FixedUpdate()
    {
        if (isTired) return;
        if (isInteracting)
        {
            animator.SetTrigger("isInteracting");
            return;
        }
        if (destination == Vector3.zero) GetNewDestination();
        else if (Vector2.Distance(go.transform.position, destination) < distanceThreshold)
        {
            mono.StartCoroutine(recordInteractTime());
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

    IEnumerator recordWorkTime ()
    {
        yield return new WaitForSeconds(workTime);
        isGettingTired = true;
    }

    IEnumerator recordRestTime ()
    {
        yield return new WaitForSeconds(restTime);
        hasRested = true;
    }

    IEnumerator recordInteractTime ()
    {
        
        observer.enabled = false;
        isInteracting = true;
        yield return new WaitForSeconds(interactTime);
        isInteracting = false;
        if (isGettingTired) isTired = true;
        else GetNewDestination();
        observer.enabled = true;
    }
}