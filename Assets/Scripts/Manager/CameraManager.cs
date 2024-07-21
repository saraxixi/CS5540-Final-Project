using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : SingleMonoBase<CameraManager>
{
    public CinemachineBrain cmBrain;

    //Get FreeLook Camera
    public GameObject freeLookCamera;

    public CinemachineFreeLook freeLook;
    public void ResetFreeLookCamera()
    {
        freeLook.m_YAxis.Value = 0.5f;
        freeLook.m_XAxis.Value = PlayerController.INSTANCE.playerModel.transform.eulerAngles.y;
    }

}
