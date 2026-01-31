using UnityEngine;
using UnityEngine.AI;
public abstract class BaseState : IState
{
    protected readonly GameObject go;
    protected MonoBehaviour mono;
    protected NavMeshAgent agent;

    public virtual void OnEnter()
    {
        mono = go.GetComponent<MonoBehaviour>();
        agent = go.GetComponent<NavMeshAgent>();
    }
    public virtual void FixedUpdate(){}
    public virtual void OnExit(){}

    protected BaseState (GameObject _go)
    {
        this.go = _go;
        mono = go.GetComponent<MonoBehaviour>();
        agent = go.GetComponent<NavMeshAgent>();
    }
}