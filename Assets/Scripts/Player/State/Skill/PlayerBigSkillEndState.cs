using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBigSkillEndState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();
        playerModel.bigSkillCamera.SetActive(false);
        CameraManager.INSTANCE.cmBrain.m_DefaultBlend = new Cinemachine.CinemachineBlendDefinition(Cinemachine.CinemachineBlendDefinition.Style.EaseInOut, 2f);
        CameraManager.INSTANCE.freeLookCamera.SetActive(true);
        CameraManager.INSTANCE.ResetFreeLookCamera();

        playerController.PlayAnimation("BigSkill_End");
    }

    public override void Update()
    {
        base.Update();

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

        #region
        if (IsAnimationEnd())
        {
            playerController.SwitchState(PlayerState.Idle);
        }
        #endregion
    }
}
