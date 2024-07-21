using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Run,
    RunEnd,
    TurnBack,
    EvadeFront,
    EvadeBack,
    EvadeEnd,
    NormalAttack,
    NormalAttackEnd,
    BigSkillStart,
    BigSkill,
    BigSkillEnd,
    JumpStart,
}

public class PlayerStateBase : StateBase
{
    protected PlayerModel playerModel;

    protected PlayerController playerController;

    protected AnimatorStateInfo stateInfo;

    protected float animationPlayTime = 0;
    public override void Init(IStateMachineOwner owner)
    {
        playerController = (PlayerController)owner;
        playerModel = playerController.playerModel;
    }
    public override void Enter()
    {
        animationPlayTime = 0;
    }

    public override void Exit()
    {
    }

    public override void FixedUpdate()
    {
    }

    public override void LateUpdate()
    {
    }

    public override void UnInit()
    {
    }

    public override void Update()
    {
        // Add gravity
        playerModel.characterController.Move(new Vector3(0, playerModel.gravity * Time.deltaTime, 0));
        // Refresh the animation stateInfo
        stateInfo = playerModel.animator.GetCurrentAnimatorStateInfo(0);
        // Animation timer
        animationPlayTime += Time.deltaTime;
    }
    public bool IsAnimationEnd()
    {
        return stateInfo.normalizedTime >= 1.0f && !playerModel.animator.IsInTransition(0);
    }
}
