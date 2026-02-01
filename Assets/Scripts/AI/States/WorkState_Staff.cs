using System.Collections;
using UnityEngine;

public class WorkState_Staff : BaseState
{
    float restTime = 15f;
    float workTime = 5f;
    public bool isTired = false;
    public bool hasRested = true;

    public WorkState_Staff (GameObject go) : base(go) {}

    public override void OnEnter()
    {
        isTired = false;
        hasRested = false;
        mono.StartCoroutine(recordWorkTime());
        Work();
    }

    public override void FixedUpdate()
    {
        // Work();
    }

    void Work()
    {
        // TODO
        agent.SetDestination(Vector3.zero);
    }

    public override void OnExit()
    {
        hasRested = false;
        mono.StartCoroutine(recordRestTime());
    }

    IEnumerator recordWorkTime ()
    {
        yield return new WaitForSeconds(workTime);
        isTired = true;
    }

    IEnumerator recordRestTime ()
    {
        yield return new WaitForSeconds(restTime);
        hasRested = true;
    }
}