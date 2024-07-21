using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerStateBase
{
    public Camera mainCamera;
    public override void Enter()
    {
        base.Enter();

        mainCamera = Camera.main;
        #region
        switch (playerModel.foot)
        { 
            case ModelFoot.Right:
                playerController.PlayAnimation("Run", 0.25f, 0.5f);
                playerModel.foot = ModelFoot.Left;
                break;
            case ModelFoot.Left:
                playerController.PlayAnimation("Run", 0.25f, 0);
                playerModel.foot = ModelFoot.Right;
                break;
        }
        #endregion
        playerController.PlayAnimation("Run");
    }
    public override void Update()
    {
        base.Update();

        #region Listen to Attack
        if (playerController.inputSystem.Player.Attack.triggered)
        {
            playerController.SwitchState(PlayerState.NormalAttack);
            return;
        }
        #endregion

        #region Listen to Evade
        if (playerController.inputSystem.Player.Evade.triggered)
        {
            playerController.SwitchState(PlayerState.EvadeFront);
            return;
        }
        #endregion

        #region Listen to BigSkill
        if (playerController.inputSystem.Player.BigSkill.triggered)
        {
            playerController.SwitchState(PlayerState.BigSkillStart);
            return;
        }
        #endregion

        #region Listen to RunEnd
        if (playerController.inputMoveVec2 == Vector2.zero)
        {
            playerController.SwitchState(PlayerState.RunEnd);
            return;
        }
        #endregion
        else 
        {
            #region Direction of Movement
            Vector3 inputMoveVec3 = new Vector3(playerController.inputMoveVec2.x, 0, playerController.inputMoveVec2.y);
            float cameraAxisY = mainCamera.transform.eulerAngles.y;
            Vector3 targetDic = Quaternion.Euler(0, cameraAxisY, 0) * inputMoveVec3;
            Quaternion targetQua = Quaternion.LookRotation(targetDic);
            float angle = Mathf.Abs(targetQua.eulerAngles.y - playerModel.transform.eulerAngles.y);
            if (angle > 177.5 && angle < 182.5)
            {
                playerController.SwitchState(PlayerState.TurnBack);
            }
            else
            {
                playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, targetQua, Time.deltaTime * playerController.rotationSpeed);
            }
            #endregion
        }


    }
}
