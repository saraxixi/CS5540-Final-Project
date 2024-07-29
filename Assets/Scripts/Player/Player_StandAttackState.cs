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

        // Check Rotate
        if (player.CurrentSkillConfig.ReleaseData.CanRotate)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            if (h != 0 || v != 0)
            {
                // 处理旋转的问题
                Vector3 input = new Vector3(h, 0, v);
                // 获取相机的旋转值 y
                float y = Camera.main.transform.rotation.eulerAngles.y;
                // 让四元数和向量相乘：表示让这个向量按照这个四元数所表达的角度进行旋转后得到新的向量
                Vector3 targetDir = Quaternion.Euler(0, y, 0) * input;
                player.Model.transform.rotation = Quaternion.Slerp(player.Model.transform.rotation, Quaternion.LookRotation(targetDir), Time.deltaTime * player.rotateSpeedForAttack);
            }
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
