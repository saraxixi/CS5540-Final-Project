using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName ="Config/Skill")]
public class SkillConfig : ScriptableObject
{
    [Header("Basic Skill Info")]
    public string AnimationName;
    public string SkillName;
    public int SkillID;
    [TextArea]
    public string SkillDescription;
    public Sprite SkillIcon;
    public int SkillLevel;
    public bool IsUnlock;

    public SkillConfig[] preSkills;
    public Skill_ReleaseData ReleaseData;
    public Skill_AttackData[] AttackData;
}


[Serializable]
public class Skill_ReleaseData
{
    // 播放粒子 / 产生的游戏物体
    public Skill_SpawnObj SpawnObj;
    // 音效
    public AudioClip AudioClip;
    // Rotation
    public bool CanRotate;
}

/// <summary>
/// 技能攻击数据
/// </summary>
[Serializable]
public class Skill_AttackData
{
    // 播放粒子 / 产生的游戏物体
    public Skill_SpawnObj SpawnObj;
    // 音效
    public AudioClip AudioClip;

    // 命中数据
    public Skill_HitData HitData;
    // 伤害数值
    public float DamgeValue;
    // 硬直时间
    public float HardTime;
    // 击飞、击退的程度
    public Vector3 RepelVelocity;
    // 击飞、击退的过渡时间
    public float RepelTime;
    // 屏幕震动
    public float ScreenImpulseValue;
    // 命中效果
    public SkillHitEFConfig SkillHitEFConfig;
}

/// <summary>
/// 技能命中数据
/// </summary>
[Serializable]
public class Skill_HitData
{
    // 伤害数值
    public float DamgeValue;
    // 硬直时间
    public float HardTime;
    // 击飞、击退的程度
    public Vector3 RepelVelocity;
    // 是否击倒
    public bool Down;
    // 击飞、击退的过渡时间
    public float RepelTime;
}


/// <summary>
/// 技能产生物体
/// </summary>
[Serializable]
public class Skill_SpawnObj
{
    // 生成的预制体
    public GameObject Prefab;
    // 生成的音效
    public AudioClip AudioClip;
    // 位置
    public Vector3 Position;
    // 旋转
    public Vector3 Rotation;
    // 延迟时间
    public float Time;
}