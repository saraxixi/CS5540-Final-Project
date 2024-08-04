using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Level : MonoBehaviour
{
    public int level = 1;
    public int experience = 0;
    public int experienceToNextLevel = 100;

    // Method to add experience points
    public void AddExperience(int amount)
    {
        experience += amount;
        CheckExperience();
    }

    // Method to check if the experience is enough to level up
    void CheckExperience()
    {
        while (experience >= experienceToNextLevel)
        {
            experience -= experienceToNextLevel;
            IncreaseLevel();
        }
    }

    public void IncreaseLevel()
    {
        level++;
        CheckLevel();
    }

    void CheckLevel()
    {
        if (level == 2)
        {
            FindObjectOfType<LevelManager>().NextLevel();
        }
    }
}

