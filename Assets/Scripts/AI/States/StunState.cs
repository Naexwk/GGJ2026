using System.Collections;
using UnityEngine;

public class StunState : BaseState
{
    float stunTime = 5f;
    public bool isNoLongerStunned = true;
    public StunState(GameObject go) : base(go) {}

    public override void OnEnter()
    {
        mono.StartCoroutine(RecordStun());
    }
    
    IEnumerator RecordStun()
    {
        yield return new WaitForSeconds(stunTime);
        isNoLongerStunned = true;
    }

    public override void OnExit()
    {
        isNoLongerStunned = false;
    }
}