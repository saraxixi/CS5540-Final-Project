using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunEndState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();

        #region Determine the left or right foot
        switch (playerModel.foot) 
        { 
            case ModelFoot.Left:
                playerController.PlayAnimation("Run_End_L", 0.1f);
                break;
            case ModelFoot.Right:
                playerController.PlayAnimation("Run_End_R", 0.1f);
                break;
        }
        #endregion
    }

    public override void Update()
    {
        base.Update();

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

        #region Listen to run
        if (playerController.inputMoveVec2 != Vector2.zero)
        {
            playerController.SwitchState(PlayerState.Run);
            return;
        }
        #endregion

        #region Animation finish or not
        if (IsAnimationEnd())
        {
            playerController.SwitchState(PlayerState.Idle);
            return;
        }
        #endregion
    }
}
