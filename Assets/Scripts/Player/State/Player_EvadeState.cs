using UnityEngine;

public class Player_EvadeState:PlayerStateBase
{
    public override void Enter()
    {
        player.PlayAnimation("Evade");
        player.Model.SetRooMotionAction(OnRootMotion);
    }
    public override void Update()
    {
        if (CheckAnimatorStateName("Evade",out float animationTime))
        {
            if (animationTime>0.8f)
            {
                player.ChangeState(PlayerState.Idle);
            }
        }
    }
    private void OnRootMotion(Vector3 deltaPosition, Quaternion deltaRotation)
    {
        deltaPosition *= Mathf.Clamp(moveStatePower,1,1.5f);
        deltaPosition.y = player.gravity * Time.deltaTime;
        player.CharacterController.Move(deltaPosition);
    }
}