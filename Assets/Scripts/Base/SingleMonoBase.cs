using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleMonoBase<T> : MonoBehaviour where T : SingleMonoBase<T> 
{
    public static T INSTANCE;

    protected virtual void Awake()
    {
        if (INSTANCE != null)
        {
            Debug.LogError(this + "Not SingleMonoBase");
        }
        INSTANCE = (T) this;
    }

    protected virtual void OnDestroy()
    {
        Destory();
    }
    public void Destory()
    { 
        INSTANCE = null;
    }
}
