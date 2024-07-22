using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ModelFoot
{
    Left,
    Right,
}
public class PlayerModel : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;

    // Player state
    [HideInInspector] public PlayerState state;

    // Character Controller
    public CharacterController characterController;

    // Gravity
    public float gravity = -9.8f;

    #region Animation State
    public ModelFoot foot = ModelFoot.Right;

    /// <summary>
    /// Âõ³ö×ó½Å
    /// </summary>
    public void SetOutLeftFoot() 
    {
        foot = ModelFoot.Left;
    }

    /// <summary>
    /// Âõ³öÓÒ½Å
    /// </summary>
    public void SetOutRightFoot()
    {
        foot = ModelFoot.Right;
    }
    #endregion

    // Skill Config
    public SkillConfig skillConfig;

    // public GameObject bigSkillStartCamera;

    // public GameObject bigSkillCamera;

}
