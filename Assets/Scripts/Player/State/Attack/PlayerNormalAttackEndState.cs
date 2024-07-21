using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNormalAttackEndState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();

        playerController.PlayAnimation("Attack_Normal_" + playerModel.skillConfig.currentAttackIndex + "_End");
    }

    public override void Update()
    {
        base.Update();

        #region Listen to attack
        if (playerController.inputSystem.Player.Attack.triggered)
        {
            playerModel.skillConfig.currentAttackIndex++;
            if (playerModel.skillConfig.currentAttackIndex >= playerModel.skillConfig.normallAttactDamageMutiple.Length + 1)
            {
                playerModel.skillConfig.currentAttackIndex = 1;
            }
            playerController.SwitchState(PlayerState.NormalAttack);
            return;
        }
        #endregion

        #region Listen to run
        if (playerController.inputMoveVec2 != Vector2.zero && animationPlayTime > 0.5f)
        {
            playerController.SwitchState(PlayerState.Run);
            playerModel.skillConfig.currentAttackIndex = 1;
        }
        #endregion

        #region Animation finish or not
        if (IsAnimationEnd())
        {
            playerController.SwitchState(PlayerState.Idle);
            playerModel.skillConfig.currentAttackIndex = 1;
        }
        #endregion
    }
}
