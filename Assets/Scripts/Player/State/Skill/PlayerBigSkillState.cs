using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBigSkillState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();

        // playerModel.bigSkillStartCamera.SetActive(false);
        // playerModel.bigSkillCamera.SetActive(true);

        playerController.PlayAnimation("BigSkill", 0f);
    }

    public override void Update()
    {
        base.Update();

        #region
        if (IsAnimationEnd())
        {
            playerController.SwitchState(PlayerState.BigSkillEnd);
        }
        #endregion
    }
}
