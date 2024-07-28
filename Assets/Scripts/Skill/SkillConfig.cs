using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Config/Skill")]
public class SkillConfig : ScriptableObject
{
    // ���ܵĶ�������
    public string AnimationName;
    public Skill_ReleaseData ReleaseData;
    public Skill_AttackData[] AttackData;
}

/// <summary>
/// �����ͷ�����
/// </summary>
[Serializable]
public class Skill_ReleaseData
{
    // �������� / ��������Ϸ����
    public Skill_SpawnObj SpawnObj;
    // ��Ч
    public AudioClip AudioClip;
}

/// <summary>
/// ���ܹ�������
/// </summary>
[Serializable]
public class Skill_AttackData
{
    // �������� / ��������Ϸ����
    public Skill_SpawnObj SpawnObj;
    // ��Ч
    public AudioClip AudioClip;

    // ��������
    public Skill_HitData HitData;
    // �˺���ֵ
    public float DamgeValue;
    // Ӳֱʱ��
    public float HardTime;
    // ���ɡ����˵ĳ̶�
    public Vector3 RepelVelocity;
    // ���ɡ����˵Ĺ���ʱ��
    public float RepelTime;
    // ��Ļ��
    public float ScreenImpulseValue;
    // ����Ч��
    public SkillHitEFConfig SkillHitEFConfig;
}

/// <summary>
/// ������������
/// </summary>
[Serializable]
public class Skill_HitData
{
    // �˺���ֵ
    public float DamgeValue;
    // Ӳֱʱ��
    public float HardTime;
    // ���ɡ����˵ĳ̶�
    public Vector3 RepelVelocity;
    // �Ƿ����
    public bool Down;
    // ���ɡ����˵Ĺ���ʱ��
    public float RepelTime;
}


/// <summary>
/// ���ܲ�������
/// </summary>
[Serializable]
public class Skill_SpawnObj
{
    // ���ɵ�Ԥ����
    public GameObject Prefab;
    // ���ɵ���Ч
    public AudioClip AudioClip;
    // λ��
    public Vector3 Position;
    // ��ת
    public Vector3 Rotation;
    // �ӳ�ʱ��
    public float Time;
}