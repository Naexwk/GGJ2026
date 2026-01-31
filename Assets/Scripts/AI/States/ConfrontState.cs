using UnityEngine;

public class ConfrontState : BaseState
{
    GameObject player;

    public ConfrontState(GameObject go) : base(go) {}

    public override void OnEnter()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void FixedUpdate()
    {
        if (player != null)
        agent.SetDestination(player.transform.position);
    }
}