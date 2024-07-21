using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvadeState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();

        switch (playerModel.state) 
        {
            case PlayerState.Run:
                playerController.PlayAnimation("Evade_F");
                break;
            case PlayerState.Idle:
            case PlayerState.RunEnd:
                playerController.PlayAnimation("Evade_B");
                break;
        }
    }

    public override void Update()
    {
        base.Update();

        #region Determian the animation finish or not
        if (stateInfo.normalizedTime >= 1.0f && !playerModel.animator.IsInTransition(0))
        {
            playerController.SwitchState(PlayerState.EvadeEnd);
        }
        #endregion
    }
}
