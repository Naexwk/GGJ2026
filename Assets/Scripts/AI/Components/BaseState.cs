using UnityEngine;
using UnityEngine.AI;

// Base state: does nothing but get all needed components for functionality
// It is NOT a MonoBehaviour, which means inherited functions won't work, like GameObject.Destroy()
// If those functions are needed, make sure to call the mono variable first
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