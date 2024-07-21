using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerBigSkillStartState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();

        // Shift Camera to BigSkill Camera
        CameraManager.INSTANCE.cmBrain.m_DefaultBlend = new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.Cut, 0f);
        CameraManager.INSTANCE.freeLookCamera.SetActive(false);
        playerModel.bigSkillStartCamera.SetActive(true);

        playerController.PlayAnimation("BigSkill_Start", 0f);
    }

    public override void Update()
    {
        base.Update();

        #region
        if (IsAnimationEnd())
        { 
            playerController.SwitchState(PlayerState.BigSkill);
        }
        #endregion
    }
}