using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();

        playerController.PlayAnimation("Idle");
    }

    public override void Update()
    {
        base.Update();

        #region Listen to Jump
        if (playerController.inputSystem.Player.Jump.triggered)
        {
            playerController.SwitchState(PlayerState.JumpStart);
            return;
        }
        #endregion

        #region Listen to BigSkill
        if (playerController.inputSystem.Player.BigSkill.triggered)
        {
            playerController.SwitchState(PlayerState.BigSkillStart);
            return;
        }
        #endregion

        #region Listen to attack
        if (playerController.inputSystem.Player.Attack.triggered)
        {
            playerController.SwitchState(PlayerState.NormalAttack);
            return;
        }
        #endregion

        #region listen to evade
        if (playerController.inputSystem.Player.Evade.triggered)
        {
            playerController.SwitchState(PlayerState.EvadeBack);
            return;
        }
        #endregion

        #region listen to run
        if (playerController.inputMoveVec2 != Vector2.zero)
        {
            playerController.SwitchState(PlayerState.Run);
            return;
        }
        #endregion
    }
}