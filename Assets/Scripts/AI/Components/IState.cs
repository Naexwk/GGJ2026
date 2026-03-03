// State interface
// All inherited classes will have these functions, to ensure states have the needed methods
// Inherited by BaseState
public interface IState
{
    void OnEnter ();
    void FixedUpdate();
    void OnExit();
}