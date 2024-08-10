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
                // ������ת������
                Vector3 input = new Vector3(h, 0, v);
                // ��ȡ�������תֵ y
                float y = Camera.main.transform.rotation.eulerAngles.y;
                // ����Ԫ����������ˣ���ʾ������������������Ԫ�������ĽǶȽ�����ת��õ��µ�����
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
