using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character Data/Data")]
public class CharacterData_SO : ScriptableObject
{
    [Header("State Info")]
    public int maxHealth;
    public int currentHealth;
    public int baseDefence;
    public int currentDefence;

    [Header("Kill")]
    public int killPoint;

    [Header("Level")]
    public int currentLevel;
    public int maxLevel;
    public int baseExp;
    public int currentExp;
    public float levelBuff;

    public float levelMultiplier
    {
        get { return 1 + (currentLevel- 1) * levelBuff; }
    }

    public void UpdateExp(int point)
    { 
        currentExp += point;

        if (currentExp >= baseExp)
        { 
            LevelUp();
        }
    }

    private void LevelUp()
    {
        currentLevel = Mathf.Clamp(currentLevel + 1, 0, maxLevel);
        currentExp -= baseExp;
        baseExp += (int)(baseExp * levelMultiplier);
        maxHealth = (int)(maxHealth * levelMultiplier);
        currentHealth = maxHealth;

        Debug.Log("Level Up" + currentLevel + "Max Health" + maxHealth);
    }
}
