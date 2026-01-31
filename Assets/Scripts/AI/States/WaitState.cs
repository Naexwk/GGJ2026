using System.Collections;
using UnityEngine;

public class WaitState : BaseState
{
    float waitTime = 2f;
    public bool hasWaited = false;
    public WaitState(GameObject go) : base(go) {}

    public override void OnEnter()
    {
        mono.StartCoroutine(Wait());
        Wait();
    }
    
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitTime);
        hasWaited = true;
    }

    public override void OnExit()
    {
        hasWaited = false;
    }
}