using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_SkillAttackState : PlayerStateBase
{
    private SkillConfig skillConfig;
    public override void Enter()
    {
        Debug.Log("Enter Skill Attack State");
        // Root motion
        player.Model.SetRooMotionAction(OnRootMotion);

    }

    public void InitData(SkillConfig skillconfig)
    {
        this.skillConfig = skillconfig;
        StartSkill();  
    }
    public override void Exit()
    { 
        player.OnSkillOver();
        skillConfig = null;
    }

    private void StartSkill()
    {
        player.StartAttack(skillConfig);
    }

    public override void Update()
    {
        // Check Idel
        if (CheckAnimatorStateName(skillConfig.AnimationName, out float animationTime) && animationTime >= 1)
        {
            // Back to idle state
            player.ChangeState(PlayerState.Idle);
            return;
        }

        // Check Attack
        if (CheckStandAttack())
        { 
            player.ChangeState(PlayerState.StandAttack);
            return;
        }

        // Check Skill
        if (player.CheckAndEnterSkillState())
        { 
            return;
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
