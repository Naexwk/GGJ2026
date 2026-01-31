using UnityEngine;

public class CharacterStateMachine : MonoBehaviour
{
    CharacterState currentState;
    [SerializeField] CharacterState[] states;

    public void TryStateChange(string stateName)
    {
        foreach (var state in states)
        {
            if (state.name == stateName)
            {
                ChangeState(state);
                return;
            }
        }
    }

    private void ChangeState(CharacterState state)
    {
        currentState.OnStateExit();
        currentState = state;
        state.OnStateEnter();
    }

    void Update()
    {
        currentState?.OnStateStay();
    }

    void Start()
    {
        currentState = states[0];
        currentState.OnStateEnter();
    }
}