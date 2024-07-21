using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoManager : SingleMonoBase<MonoManager>
{
    public Action updateAction;
    public Action fixedUpdateAction;
    public Action lateUpdateAction;

    /// <summary>
    /// Add a task to the updateAction
    /// </summary>
    /// <param name="task"></param>
    public void AddUpdateAction(Action task)
    {
        updateAction += task;
    }

    /// <summary>
    /// Add a task to the fixedUpdateAction
    /// </summary>
    /// <param name="task"></param>
    public void AddFixedUpdateAction(Action task)
    {
        fixedUpdateAction += task;
    }


    /// <summary>
    /// Add a task to the lateUpdateAction
    /// </summary>
    /// <param name="task"></param>
    public void AddLateUpdateAction(Action task)
    {
        lateUpdateAction += task;
    }

    /// <summary>
    /// Remove a task from the updateAction
    /// </summary>
    /// <param name="task"></param>
    public void RemoveUpdateAction(Action task)
    {
        updateAction -= task;
    }

    /// <summary>
    /// Remove a task from the fixedUpdateAction
    /// </summary>
    /// <param name="task"></param>

    public void RemoveFixedUpdateAction(Action task)
    {
        fixedUpdateAction -= task;
    }

    /// <summary>
    /// Remove a task from the lateUpdateAction
    /// </summary>
    /// <param name="task"></param>
    public void RemoveLateUpdateAction(Action task)
    {
        lateUpdateAction -= task;
    }

    // Update is called once per frame
    void Update()
    {
        updateAction?.Invoke();
    }
    void FixedUpdate()
    {
        fixedUpdateAction?.Invoke();
    }
    void LateUpdate()
    {
        lateUpdateAction?.Invoke();
    }
}
