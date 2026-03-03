// Transition interface
// All inherited classes will have these functions, to ensure transitions have the needed methods
public interface ITransition
{
    IState to {get;}
    IPredicate condition {get;}
}