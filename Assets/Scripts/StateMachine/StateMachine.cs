using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateMachineOwner { }


public class StateMachine
{
    // Current state
    private StateBase currentState;

    // Check if there is current state
    public bool HasState { get => currentState != null; }

    private IStateMachineOwner owner;

    //State Dictionary
    private Dictionary<Type, StateBase> stateDic = new Dictionary<Type, StateBase>();

    public StateMachine(IStateMachineOwner owner) 
    {
        Init(owner);
    }

    //Initialize the state machine
    public void Init(IStateMachineOwner owner)
    {
        this.owner = owner;
    }
    /// <summary>
    /// Switch to a new state
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void EnterState<T>() where T : StateBase, new()
    {
        // If the current state is the same as the new state, not switch to new state
        if (HasState && currentState.GetType() == typeof(T))
        {
            return;
        }

        #region finish current state
        if (HasState)
        {
            currentState.Exit();
            ExitCurrentState();

        }
        #endregion

        #region switch to new state
        currentState = LoadState<T>();
        EnterCurrentState();
        #endregion
    }

    /// <summary>
    /// Load or add a state
    /// </summary>
    /// <typeparam name="T">State Type</typeparam>
    /// <returns></returns>
    private StateBase LoadState<T>() where T : StateBase, new()
    {
        Type stateType = typeof(T);

        if (!stateDic.TryGetValue(stateType, out StateBase state))
        { 
            state = new T();
            state.Init(owner);
            stateDic.Add(stateType, state);
        }

        return state;
    }

    private void EnterCurrentState()
    {
        currentState.Enter();
        MonoManager.INSTANCE.AddUpdateAction(currentState.Update);
        MonoManager.INSTANCE.AddFixedUpdateAction(currentState.FixedUpdate);
        MonoManager.INSTANCE.AddLateUpdateAction(currentState.LateUpdate);
    }

    public void ExitCurrentState()
    {
        currentState.Exit();
        MonoManager.INSTANCE.RemoveUpdateAction(currentState.Update);
        MonoManager.INSTANCE.RemoveFixedUpdateAction(currentState.FixedUpdate);
        MonoManager.INSTANCE.RemoveLateUpdateAction(currentState.LateUpdate);

    }

    /// <summary>
    /// Stop the state machine
    /// </summary>
    public void Stop()
    { 
        ExitCurrentState();
        foreach (var item in stateDic.Values)
        {
            item.UnInit();
        }
        stateDic.Clear();
    }
}
