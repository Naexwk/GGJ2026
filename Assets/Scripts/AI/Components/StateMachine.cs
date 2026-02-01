using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    StateNode current;
    IState defaultState;
    Dictionary<Type, StateNode> nodes = new();
    HashSet<ITransition> anyTransitions = new();

    public StateMachine (IState _defaultState)
    {
        if (GetOrAddNode(_defaultState) != null)
        {
            defaultState = _defaultState;
            SetState(defaultState);
        }
    }

    public void FixedUpdate ()
    {
        var transition = GetTransition();
        if (transition != null) ChangeState(transition.to);
        current.State?.FixedUpdate();
    }

    public void SetState(IState state)
    {
        current = nodes[state.GetType()];
        current.State?.OnEnter();
    }

    void ChangeState(IState state)
    {
        if (state == current.State) return;

        Debug.Log($"Changing state to {state}");

        var previousState = current.State;
        var nextState = nodes[state.GetType()].State;

        previousState?.OnExit();
        nextState?.OnEnter();
        current = nodes[state.GetType()];
    }

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

    public void AddTransition(IState from, IState to, IPredicate condition)
    {
        GetOrAddNode(from).AddTransition(GetOrAddNode(to).State, condition);
    }

    public void AddAnyTransition(IState to, IPredicate condition)
    {
        anyTransitions.Add(new Transition(GetOrAddNode(to).State, condition));
    }

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