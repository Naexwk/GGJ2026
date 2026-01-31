public abstract class CharacterState
{
    public string name;
    public abstract void StateActivity();
    public abstract void OnStateEnter();
    public abstract void OnStateStay();
    public abstract void OnStateExit();
}
