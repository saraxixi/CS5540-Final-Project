using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New AttackData", menuName = "Character Data/Attack Data")]
public class AttackData_SO : ScriptableObject
{
    public int minDamage;
    public int maxDamage;
    public float criticalMultiplier;
    public float criticalChance;
}
