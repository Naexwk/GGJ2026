using System.Collections;
using UnityEngine;

public class StunState : BaseState
{
    float stunTime = 5f;
    public bool hasWaitedStun = false;
    public StunState(GameObject go) : base(go) {}

    public override void OnEnter()
    {
        hasWaitedStun = false;
        mono.StartCoroutine(RecordStun());
    }
    
    IEnumerator RecordStun()
    {
        yield return new WaitForSeconds(stunTime);
        hasWaitedStun = true;
    }

    public override void OnExit()
    {
        hasWaitedStun = false;
        Debug.Log("Exit stun");
    }
}