using UnityEngine;
using UnityEngine.UIElements;

public class Player_AirDownState : PlayerStateBase
{
    private enum AirDownChildState
    { 
        Loop,
        End
    }

    private float endAnimationHeight = 3f;     // End����������Ҫ�ĸ߶ȣ�������߶ȿ�ʼ����

    private LayerMask groundLayerMask = LayerMask.GetMask("Ground");
    private bool needEndAnimation;
    private AirDownChildState airDownState;
    private AirDownChildState AirDownState
    {
        get => airDownState;
        set {
            airDownState = value;
            switch (airDownState)
            {
                case AirDownChildState.Loop:
                    player.PlayAnimation("JumpLoop");
                    break;
                case AirDownChildState.End:
                    player.PlayAnimation("JumpEnd");
                    break;
            }
        }
    }
    public override void Enter()
    {
        AirDownState = AirDownChildState.Loop;
        // �жϵ�ǰ��ɫ�ĸ߶��Ƿ��п����л���End
        needEndAnimation = !Physics.Raycast(player.transform.position + new Vector3(0, 0.5f, 0), player.transform.up * -1, endAnimationHeight + 0.5f);
    }

    public override void Update()
    {

        switch (airDownState)
        {
            case AirDownChildState.Loop:

                if (needEndAnimation)
                {
                    // �߶ȼ��
                    if (Physics.Raycast(player.transform.position + new Vector3(0, 0.5f, 0), player.transform.up * -1, endAnimationHeight + 0.5f))
                    {

                        Debug.Log("Switching to JumpEnd animation.");
                        AirDownState = AirDownChildState.End;
                    }
                }
                else
                {
                    if (player.CharacterController.isGrounded)
                    {
                        player.ChangeState(PlayerState.Idle);
                        return;
                    }
                }
                AirControl();

                break;
            case AirDownChildState.End:
                if (CheckAnimatorStateName("JumpEnd",out float animationTime))
                {
                    if (animationTime>=0.8f)
                    {
                        // ��ʱ��Ȼ�ȿ��ˣ�������׹
                        if (player.CharacterController.isGrounded == false)
                        {
                            AirDownState = AirDownChildState.Loop;
                        }
                        else
                        {
                            player.ChangeState(PlayerState.Idle);
                        }
                    }
                    else if(animationTime<0.6f)
                    {
                        AirControl();
                    }   
                }

                break;
        }
    }

    private void AirControl()
    {
        // �������λ��
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 motion = new Vector3(0, player.gravity * Time.deltaTime, 0);
        if (h != 0 || v != 0)
        {
            Vector3 input = new Vector3(h, 0, v);
            Vector3 dir = Camera.main.transform.TransformDirection(input);
            motion.x = player.moveSpeedForAirDown * Time.deltaTime * dir.x;
            motion.z = player.moveSpeedForAirDown * Time.deltaTime * dir.z;
            // ������ת
            // ��ȡ�������תֵ y
            float y = Camera.main.transform.rotation.eulerAngles.y;
            // ����Ԫ����������ˣ���ʾ������������������Ԫ�������ĽǶȽ�����ת��õ��µ�����
            Vector3 targetDir = Quaternion.Euler(0, y, 0) * input;
            player.Model.transform.rotation = Quaternion.Slerp(player.Model.transform.rotation, Quaternion.LookRotation(targetDir), Time.deltaTime * player.rotateSpeed);
        }

        player.CharacterController.Move(motion);
    }
}
