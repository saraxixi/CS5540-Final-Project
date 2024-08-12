using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : MonoBehaviour
{
    public CharacterData_SO templateData;
    public CharacterData_SO characterData;
    public AttackData_SO attackData;


    [HideInInspector]
    public bool isCritical;

    private void Awake()
    {
        if (templateData != null)
        {
            characterData = Instantiate(templateData);
        }
    }

    #region Ream from Data SO
    public int maxHealth { 
        get { if (characterData != null) return characterData.maxHealth; else return 0;}
        set { characterData.maxHealth = value;}
    }

    public int currentHealth
    {
        get { if (characterData != null) return characterData.currentHealth; else return 0; }
        set { characterData.currentHealth = value; }
    }

    public int baseDefence
    {
        get { if (characterData != null) return characterData.baseDefence; else return 0; }
        set { characterData.baseDefence = value; }
    }

    public int currentDefence
    {
        get { if (characterData != null) return characterData.currentDefence; else return 0; }
        set { characterData.currentDefence = value; }
    }
    #endregion

    #region Character Combat
    public void TakeDamage(int attacker, CharacterState defener)
    { 
        int damage = Mathf.Max(attacker - defener.currentDefence, 0);
        currentHealth = Mathf.Max(currentHealth - damage, 0);

        //TODO: Update UI
        //TODO: ExpUpdate
        //if (currentHealth <= 0)
        //{
        //    attacker.characterDate.UpdateExp(characterData.killPoint);
        //}
    }

    public int CurrentDamage()
    {
        isCritical = UnityEngine.Random.value < attackData.criticalChance;
        float coreDamage = UnityEngine.Random.Range(attackData.minDamage, attackData.maxDamage);

        if (isCritical)
        { 
            coreDamage *= attackData.criticalMultiplier;
        }

        return (int)coreDamage;
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log("Player healed. Current health: " + currentHealth);
    }

    public void Die()
    {
        Debug.Log("Player died");
        GameManager.Instance.LevelLost();
    }
    #endregion

}
