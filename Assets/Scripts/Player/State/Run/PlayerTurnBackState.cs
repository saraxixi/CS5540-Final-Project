using System.Collections;
using System.Collections.Generic;
using Unity.XR.OpenVR;
using UnityEngine;

public class PlayerTurnBackState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();

        playerController.PlayAnimation("TurnBack", 0.1f);
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

        #region Check the animation is finished
        if (stateInfo.normalizedTime >= 1.0f && !playerModel.animator.IsInTransition(0))
        { 
            playerController.SwitchState(PlayerState.Run);
        }
        #endregion
    }
}
