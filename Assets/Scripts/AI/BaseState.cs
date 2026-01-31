public abstract class BaseState : IState
{
    //protected readonly 
    public virtual void OnEnter(){}
    public virtual void FixedUpdate(){}
    public virtual void OnExit(){}
}