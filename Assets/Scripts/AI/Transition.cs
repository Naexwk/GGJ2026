public class Transition : ITransition
{
    public IState to {get;}
    public IPredicate condition {get;}

    public Transition(IState _to, IPredicate _condition)
    {
        to = _to;
        condition = _condition;
    }
}