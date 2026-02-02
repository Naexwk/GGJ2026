using UnityEngine;
using UnityEngine.AI;
public abstract class BaseState : IState
{
    protected readonly GameObject go;
    protected MonoBehaviour mono;
    public NavMeshAgent agent;
    public Observer observer;
    public Animator animator;

    public virtual void OnEnter()
    {
        mono = go.GetComponent<MonoBehaviour>();
        agent = go.GetComponent<NavMeshAgent>();
        observer = go.GetComponent<Observer>();
        animator = go.GetComponentInChildren<Animator>();
    }
    
    public virtual void FixedUpdate(){}
    public virtual void OnExit(){}

    protected BaseState (GameObject _go)
    {
        this.go = _go;
        mono = go.GetComponent<MonoBehaviour>();
        agent = go.GetComponent<NavMeshAgent>();
        observer = go.GetComponent<Observer>();
        animator = go.GetComponentInChildren<Animator>();
    }
}