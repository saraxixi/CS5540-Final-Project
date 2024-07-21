using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpStartState :PlayerStateBase
{
    // private float jumpForce = 10f;
    public override void Enter()
    {
        base.Enter();

        playerController.PlayAnimation("Jump_Start");

    }

    public override void Update()
    {
        base.Update();

        #region Listen to Idle
        if (IsAnimationEnd())
        {
            playerController.SwitchState(PlayerState.Idle);
            return;
        }
        #endregion


    }

}
