using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

[CreateAssetMenu (menuName = "Config/Skill")]
public class SkillConfig : ScriptableObject
{
    public int currentAttackIndex = 1;
    public float[] normallAttactDamageMutiple;
}
