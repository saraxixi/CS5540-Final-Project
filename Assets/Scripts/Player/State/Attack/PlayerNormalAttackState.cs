using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNormalAttackState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();

        playerController.PlayAnimation("Attack_Normal_" + playerModel.skillConfig.currentAttackIndex);
    }

    public override void Update()
    {
        base.Update();

        #region Animation finish or not
        if (IsAnimationEnd())
        { 
            playerController.SwitchState(PlayerState.NormalAttackEnd);
        }
        #endregion

    }
}
