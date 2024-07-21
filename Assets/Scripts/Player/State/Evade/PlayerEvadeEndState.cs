using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvadeEndState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();

        switch (playerModel.state)
        {
            case PlayerState.EvadeFront:
                playerController.PlayAnimation("Evade_F_End");
                break;
            case PlayerState.EvadeBack:
                playerController.PlayAnimation("Evade_B_End");
                break;
        }
    }

    public override void Update()
    {
        base.Update();

        #region Listen to run
        if (playerController.inputMoveVec2 != Vector2.zero)
        {
            playerController.SwitchState(PlayerState.Run);
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

        #region Animation finish or not
        if (IsAnimationEnd())
        {
            playerController.SwitchState(PlayerState.Idle);
        }
        #endregion
    }
}
