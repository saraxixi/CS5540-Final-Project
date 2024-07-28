using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ״̬����
/// </summary>
public abstract class StateBase
{
    /// <summary>
    /// ��ʼ��
    /// ֻ��״̬��һ�δ�����ʱ�����
    /// </summary>
    /// <param name="owner">����</param>
    /// <param name="stateType">��ǰ���״̬�����ʵ��ö�ٵ�intֵ</param>
    public virtual void Init(IStateMachineOwner owner) { }

    /// <summary>
    /// ����ʼ��
    /// ����ʱ�ͷ�һЩ��Դ
    /// </summary>
    public virtual void UnInit() { }

    /// <summary>
    /// ״̬ÿ�ν��붼ִ��һ��
    /// </summary>
    public virtual void Enter() { }

    /// <summary>
    /// ״̬�˳�ʱִ��һ��
    /// </summary>
    public virtual void Exit() { }
    public virtual void Update() { }
    public virtual void LateUpdate() { }
    public virtual void FxiedUpdate() { }
}
