using UnityEngine;

public class PlayerStateBase : StateBase
{
    protected Player_Controller player;
    protected static float moveStatePower;
    public override void Init(IStateMachineOwner owner)
    {
        base.Init(owner);
        player = (Player_Controller)owner;
    }

    protected virtual bool CheckAnimatorStateName(string stateName,out float normalizedTime)
    {
        AnimatorStateInfo nextinfo = player.Model.Animator.GetNextAnimatorStateInfo(0);
        if (nextinfo.IsName(stateName))
        { 
            normalizedTime = nextinfo.normalizedTime;
            return true;
        }

        AnimatorStateInfo currentinfo = player.Model.Animator.GetCurrentAnimatorStateInfo(0);
        normalizedTime = currentinfo.normalizedTime;
        return currentinfo.IsName(stateName);
    }
}
