using UnityEngine;

public class RunState : BaseState
{
    GameObject[] securityTeam;
    GameObject target;
    public RunState(GameObject go) : base(go) {}

    public override void OnEnter()
    {
        securityTeam = GameObject.FindGameObjectsWithTag("Security");

        float minDistance = float.MaxValue;
        foreach (var sec in securityTeam)
        {
            float distance = Vector2.Distance(go.transform.position, sec.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                target = sec;
            }
        }
    }

    public override void FixedUpdate()
    {
        if (target != null)
        agent.SetDestination(target.transform.position);
    }
}