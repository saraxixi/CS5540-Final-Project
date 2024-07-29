using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour,IStateMachineOwner, ISkillOwner
{
    [SerializeField] private Player_Model player_Model;
    public Player_Model Model { get => player_Model; }

    [SerializeField] private CharacterController characterController;
    public CharacterController CharacterController { get => characterController; }
    [SerializeField] private AudioSource audioSource;
    private StateMachine stateMachine;


    #region 配置类型的信息
    [Header("配置")]
    public float gravity = -9.8f;
    public float rotateSpeed = 5;
    public float rotateSpeedForAttack = 5;
    public float walk2RunTransition = 1;
    public float walkSpeed = 1;
    public float runSpeed = 1;
    public float jumpPower = 1;
    public float moveSpeedForJump;
    public float moveSpeedForAirDown;
    public AudioClip[] footStepAudioClips;
    public List<string> enemyTagList;

    public SkillConfig[] standAttackConfig;

    public List<SkillInfo> skillInfoList = new List<SkillInfo>();
    #endregion

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Model.Init(OnFootStep, this, enemyTagList);
        CanSwitchSkill = true;
        stateMachine = new StateMachine();
        stateMachine.Init(this);
        ChangeState(PlayerState.Idle); // 默认进入待机状态

    }

    private void Update()
    {
        UpdateSkillCDTime();
    }

    public void ChangeState(PlayerState playerState, bool reCurrstate = false)
    {
        switch (playerState)
        {
            case PlayerState.Idle:
                stateMachine.ChangeState<Player_IdleState>(reCurrstate);
                break;
            case PlayerState.Move:
                stateMachine.ChangeState<Player_MoveState>(reCurrstate);
                break;
            case PlayerState.Jump:
                stateMachine.ChangeState<Player_JumpState>(reCurrstate);
                break;
            case PlayerState.AirDown:
                stateMachine.ChangeState<Player_AirDownState>(reCurrstate);
                break;
            case PlayerState.Evade:
                stateMachine.ChangeState<Player_EvadeState>(reCurrstate);
                break;
            case PlayerState.StandAttack:
                stateMachine.ChangeState<Player_StandAttackState>(reCurrstate);
                break;
            case PlayerState.SkillAttack:
                stateMachine.ChangeState<Player_SkillAttackState>(reCurrstate);
                break;
        }
    }

    #region Skill
    public SkillConfig CurrentSkillConfig { get; private set; }
    private int currentHitIndex = 0;
    public bool CanSwitchSkill { get; private set; }
    public void StartAttack(SkillConfig skillConfig)
    { 
        CanSwitchSkill = false;
        CurrentSkillConfig = skillConfig;
        currentHitIndex = 0;
        PlayAnimation(CurrentSkillConfig.AnimationName);

        // Play Spawn Object
        SpawnSkillObject(CurrentSkillConfig.ReleaseData.SpawnObj);

        // Play Skill Audio
        PlayAudio(CurrentSkillConfig.ReleaseData.AudioClip);

    }
    public void StartSkillHit(int weaponIndex)
    {
        Debug.Log(currentHitIndex);

        // Skill release audio
        SpawnSkillObject(CurrentSkillConfig.AttackData[currentHitIndex].SpawnObj);


        // Skill release object
        PlayAudio(CurrentSkillConfig.AttackData[currentHitIndex].AudioClip);
    }

    public void StopSkillHit(int weaponIndex)
    {
        currentHitIndex += 1;
    }

    public void SkillCanSwitch()
    {
        CanSwitchSkill = true;
    }

    public void OnSkillOver()
    { 
        CanSwitchSkill = true;
    }
    #endregion

    public void PlayAnimation(string animationName, float fixedTransitionDuration = 0.25f)
    {
        player_Model.Animator.CrossFadeInFixedTime(animationName, fixedTransitionDuration);
    }

    private void OnFootStep()
    {
        audioSource.PlayOneShot(footStepAudioClips[Random.Range(0, footStepAudioClips.Length)]);
    }

    public void PlayAudio(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }

    private void SpawnSkillObject(Skill_SpawnObj spawnObject)
    {
        if (spawnObject != null && spawnObject.Prefab != null)
        {
            StartCoroutine(DoSpawnObject(spawnObject));
        }
    }

    private IEnumerator DoSpawnObject(Skill_SpawnObj spawnObject)
    {
        yield return new WaitForSeconds(spawnObject.Time);
        GameObject skillObject = GameObject.Instantiate(spawnObject.Prefab, null);

        skillObject.transform.position = Model.transform.position + spawnObject.Position;
        skillObject.transform.eulerAngles = Model.transform.eulerAngles + spawnObject.Rotation;
        PlayAudio(spawnObject.AudioClip);
    }

    protected IEnumerator DoSkillHitEF(Skill_SpawnObj spawnObj, Vector3 spawnPoint)
    {
        if (spawnObj == null) yield break;

        if (spawnObj != null && spawnObj.Prefab != null)
        {
            // 延迟时间
            yield return new WaitForSeconds(spawnObj.Time);
            GameObject temp = Instantiate(spawnObj.Prefab);
            temp.transform.position = spawnPoint + spawnObj.Position;
            temp.transform.LookAt(Camera.main.transform);
            temp.transform.eulerAngles += spawnObj.Rotation;
            PlayAudio(spawnObj.AudioClip);
        }
    }
    public void OnHit(IHurt target, Vector3 hitPosition)
    {
        if (target == null)
        {
            Debug.LogError("Target is null");
            return;
        }

        Skill_AttackData attackData = CurrentSkillConfig.AttackData[currentHitIndex];
        Debug.Log("Damage: " + attackData.DamgeValue); // Debug damage value

        if (target.Hurt(attackData.DamgeValue, this))
        {
            // StartCoroutine(DoSkillHitEF(attackData.SkillHitEFConfig.SpawnObject, hitPosition));
        }
        else
        {
            // StartCoroutine(DoSkillHitEF(attackData.SkillHitEFConfig.FailSpawnObject, hitPosition));
        }
    }

    /// <summary>
    /// Check and enter skill state
    /// </summary>
    /// <returns></returns>
    public bool CheckAndEnterSkillState()
    {
        if (!CanSwitchSkill) return false;

        // Check skill cd time and check skill key down
        for (int i = 0; i < skillInfoList.Count; i++)
        {
            Debug.Log("CheckAndEnterSkillState");
            if (skillInfoList[i].currentTime == 0 && Input.GetKeyDown(skillInfoList[i].keyCode))
            {
                // Realse skill
                ChangeState(PlayerState.SkillAttack, true);
                Player_SkillAttackState skillAttackState = (Player_SkillAttackState)stateMachine.CurrentState;
                skillAttackState.InitData(skillInfoList[i].skillConfig);

                // Skill cd time
                skillInfoList[i].currentTime = skillInfoList[i].cdTime;
                return true;
            }
        }
        return false;
    }

    private void UpdateSkillCDTime()
    {
        for (int i = 0; i < skillInfoList.Count; i++)
        {
            skillInfoList[i].currentTime = Mathf.Clamp(skillInfoList[i].currentTime - Time.deltaTime, 0, skillInfoList[i].cdTime);
            skillInfoList[i].cdmMaskImage.fillAmount = skillInfoList[i].currentTime / skillInfoList[i].cdTime;
        }
    }
}
