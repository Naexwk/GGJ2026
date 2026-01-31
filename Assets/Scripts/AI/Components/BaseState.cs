using UnityEngine;
using UnityEngine.AI;
public abstract class BaseState : IState
{
    protected readonly GameObject go;
    protected readonly NavMeshAgent agent;

    public virtual void OnEnter(){}
    public virtual void FixedUpdate(){}
    public virtual void OnExit(){}

    protected BaseState (GameObject _go, NavMeshAgent _agent)
    {
        this.go = _go;
        this.agent = _agent;
    }
}