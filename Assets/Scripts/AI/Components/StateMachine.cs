using System;
using System.Collections.Generic;

// State machine class
// Rules states and its transitions
public class StateMachine
{
    public StateNode current;
    IState defaultState;
    Dictionary<Type, StateNode> nodes = new();
    HashSet<ITransition> anyTransitions = new();

    // Create a new State Machine with a default state
    public StateMachine (IState _defaultState)
    {
        if (GetOrAddNode(_defaultState) != null)
        {
            defaultState = _defaultState;
            SetState(defaultState);
        }
    }

    // Do behaviour, check transition conditions
    public void FixedUpdate ()
    {
        var transition = GetTransition();
        if (transition != null) ChangeState(transition.to);
        current.State?.FixedUpdate();
    }

    // Set state manually
    public void SetState(IState state)
    {
        current = nodes[state.GetType()];
        current.State?.OnEnter();
    }

    // Transition from state A to B
    void ChangeState(IState state)
    {
        if (state == current.State) return;

        var previousState = current.State;
        var nextState = nodes[state.GetType()].State;

        previousState?.OnExit();
        nextState?.OnEnter();
        current = nodes[state.GetType()];
    }

    // Get a transition whose condition has been met
    ITransition GetTransition()
    {
        foreach(var transition in anyTransitions)
        {
            if (transition.condition.Evaluate())
            {
                return transition;
            }
        }

        foreach(var transition in current.Transitions)
        {
            if (transition.condition.Evaluate())
            {
                return transition;
            }
        }

        return null;
    }

    // Add a transition from a state to another, with its transition condition
    public void AddTransition(IState from, IState to, IPredicate condition)
    {
        GetOrAddNode(from).AddTransition(GetOrAddNode(to).State, condition);
    }

    // Add a transition to a state, ignoring the current state, with its transition condition
    public void AddAnyTransition(IState to, IPredicate condition)
    {
        anyTransitions.Add(new Transition(GetOrAddNode(to).State, condition));
    }

    // Add nodes to the state machine
    StateNode GetOrAddNode(IState state)
    {
        var node = nodes.GetValueOrDefault(state.GetType());

        if(node == null)
        {
            node = new StateNode(state);
            nodes.Add(state.GetType(), node);
        }

        return node;
    }
}