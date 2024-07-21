using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SingleMonoBase<PlayerController>, IStateMachineOwner
{
    // Input System
    [HideInInspector] public InputSystem inputSystem;

    // Player Move Input
    public Vector2 inputMoveVec2;
    
    // Player Model
    public PlayerModel playerModel;

    // State Machine
    private StateMachine stateMachine;

    // Rotate Speed
    public float rotationSpeed = 8f;

    // Evade Timer
    private float evadeTimer = 1f;

    protected override void Awake()
    {
        base .Awake();

        stateMachine = new StateMachine(this);
        inputSystem = new InputSystem();
    }

    private void Start()
    {
        LockMouse();
        SwitchState(PlayerState.Idle);
    }

    /// <summary>
    /// Switch player state
    /// </summary>
    /// <param name="playerState"></param>
    public void SwitchState(PlayerState playerState)
    {
        switch (playerState)
        { 
            case PlayerState.Idle:
                stateMachine.EnterState<PlayerIdleState>();
                break;
            case PlayerState.Run:
                stateMachine.EnterState<PlayerRunState>();
                break;
            case PlayerState.RunEnd:
                stateMachine.EnterState<PlayerRunEndState>();
                break;
            case PlayerState.TurnBack:
                stateMachine.EnterState<PlayerTurnBackState>();
                break;
            case PlayerState.EvadeFront:
            case PlayerState.EvadeBack:
                if (evadeTimer != 1f)
                {
                    return;
                }
                stateMachine.EnterState<PlayerEvadeState>();
                evadeTimer -= 1f;
                break;
            case PlayerState.EvadeEnd:
                stateMachine.EnterState<PlayerEvadeEndState>();
                break;
            case PlayerState.NormalAttack:
                stateMachine.EnterState<PlayerNormalAttackState>();
                break;
            case PlayerState.NormalAttackEnd:
                stateMachine.EnterState<PlayerNormalAttackEndState>();
                break;
            case PlayerState.BigSkillStart:
                stateMachine.EnterState<PlayerBigSkillStartState>();
                break;
            case PlayerState.BigSkill:
                stateMachine.EnterState<PlayerBigSkillState>();
                break;
            case PlayerState.BigSkillEnd:
                stateMachine.EnterState<PlayerBigSkillEndState>();
                break;
            case PlayerState.JumpStart:
                stateMachine.EnterState<PlayerJumpStartState>();
                break;
        }
        playerModel.state = playerState;
    }

    /// <summary>
    /// Play animation
    /// </summary>
    /// <param name="animationName"></param>
    /// <param name="fixedTransitionDuration"></param>
    public void PlayAnimation(string animationName, float fixedTransitionDuration = 0.25f)
    {
        playerModel.animator.CrossFadeInFixedTime(animationName, fixedTransitionDuration);
    }

    public void PlayAnimation(string animationName, float fixedTransitionDuration, float fixedTimerOffSet)
    { 
        playerModel.animator.CrossFadeInFixedTime(animationName, fixedTransitionDuration, 0, fixedTimerOffSet);
    }
    private void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Update Player Move Input
        inputMoveVec2 = inputSystem.Player.Move.ReadValue<Vector2>().normalized;

        // Update Evade Timer
        if (evadeTimer < 1f)
        {
            evadeTimer += Time.deltaTime;
            if (evadeTimer >= 1f)
            {
                evadeTimer = 1f;
            }
        }
    }

    private void OnEnable()
    {
        inputSystem.Enable();
    }

    private void OnDisable()
    {
        inputSystem.Disable();
    }

}
