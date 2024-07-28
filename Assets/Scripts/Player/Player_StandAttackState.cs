using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_StandAttackState : PlayerStateBase
{
    // Current Attack
    private int currentAttackIndex;

    private int CurrentAttackIndex
    { 
        get => currentAttackIndex;
        set
        {
            if (value >= player.standAttackConfig.Length) currentAttackIndex = 0;
            else currentAttackIndex = value;
        }
    }

    public override void Enter()
    {
        CurrentAttackIndex = -1;
        // Root motion
        player.Model.SetRooMotionAction(OnRootMotion);

        // Play the character's stand attack animation
        StandAttack();
    }

    public override void Exit()
    { 
        player.OnSkillOver();
    }

    private void StandAttack()
    {
        Debug.Log(CurrentAttackIndex);
        // TODO: Continue Attack
        CurrentAttackIndex += 1;
        player.StartAttack(player.standAttackConfig[CurrentAttackIndex]);



    }

    public override void Update()
    {
        // Check Skill
        if (player.CheckAndEnterSkillState())
        {
            return;
        }

        // Check Idel
        if (CheckAnimatorStateName(player.standAttackConfig[CurrentAttackIndex].AnimationName, out float animationTime) && animationTime >= 1)
        {
            // Back to idle state
            player.ChangeState(PlayerState.Idle);
            return;
        }

        // Check Attack
        if (CheckStandAttack())
        { 
            StandAttack();
        }
    }

    private bool CheckStandAttack()
    {
        return Input.GetMouseButtonDown(0) && player.CanSwitchSkill;
    }


    private void OnRootMotion(Vector3 deltaPosition, Quaternion deltaRotation)
    {
        deltaPosition.y = player.gravity * Time.deltaTime;
        player.CharacterController.Move(deltaPosition);
    }
}
