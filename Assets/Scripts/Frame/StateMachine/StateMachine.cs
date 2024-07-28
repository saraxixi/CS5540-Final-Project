using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateMachineOwner { }
public class StateMachine
{
    private IStateMachineOwner owner;

    private Dictionary<Type, StateBase> stateDic = new Dictionary<Type, StateBase>();

    public Type CurrentStateType { get => currentState.GetType(); }
    public bool HasState { get => currentState != null; }

    private StateBase currentState;
    public StateBase CurrentState { get => currentState; }

    /// <summary>
    /// ��ʼ��
    /// </summary>
    public void Init(IStateMachineOwner owner)
    {
        this.owner = owner;
    }

    /// <summary>
    /// �л�״̬
    /// </summary>
    /// <typeparam name="T">����Ҫ�л�����״̬����</typeparam>
    /// <param name="reCurrstate">���״̬û�䣬�Ƿ���Ҫˢ��״̬</param>
    /// <returns></returns>
    public bool ChangeState<T>(bool reCurrstate = false)where T:StateBase,new()
    {
        // ״̬һ�£����Ҳ���Ҫˢ��״̬������Ҫ�����л�
        if (HasState && CurrentStateType == typeof(T) && !reCurrstate) return false;

        // �˳���ǰ״̬
        if (currentState!=null)
        {
            currentState.Exit();
            MonoManager.Instance.RemoveUpdateListener(currentState.Update);
            MonoManager.Instance.RemoveLateUpdateListener(currentState.LateUpdate);
            MonoManager.Instance.RemoveFixedUpdateListener(currentState.FxiedUpdate);
        }

        // ������״̬
        currentState = GetState<T>();
        currentState.Enter();
        MonoManager.Instance.AddUpdateListener(currentState.Update);
        MonoManager.Instance.AddLateUpdateListener(currentState.LateUpdate);
        MonoManager.Instance.AddFixedUpdateListener(currentState.FxiedUpdate);
        return false;
    }

    private StateBase GetState<T>() where T : StateBase, new()
    {
        Type type = typeof(T);
        // �����ֵ������������
        if (!stateDic.TryGetValue(type, out StateBase state))
        {
            state = new T();
            state.Init(owner);
            stateDic.Add(type, state);
        }
        return state;
    }

    /// <summary>
    /// ֹͣ�������ͷ���Դ
    /// </summary>
    public void Stop()
    {
        currentState.Exit();
        MonoManager.Instance.RemoveUpdateListener(currentState.Update);
        MonoManager.Instance.RemoveLateUpdateListener(currentState.LateUpdate);
        MonoManager.Instance.RemoveFixedUpdateListener(currentState.FxiedUpdate);
        currentState = null;

        foreach (var item in stateDic.Values)
        {
            item.UnInit();
        }
        stateDic.Clear();
    }
}
