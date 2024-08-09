using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    Text levelText;
    Image healthSlider;
    Image expSlider;
    void Awake()
    {
        levelText = transform.GetChild(2).GetChild(0).GetComponent<Text>();
        healthSlider = transform.GetChild(0).GetComponent<Image>();
        expSlider = transform.GetChild(1).GetComponent<Image>();
    }

    void Update()
    {
        levelText.text = "Level " + GameManager.Instance.playerState.characterData.currentLevel.ToString("00");
        UpdateHealth();
        UpdateExp();
    }

    void UpdateHealth()
    { 
        float sliderPercent = (float)GameManager.Instance.playerState.currentHealth / GameManager.Instance.playerState.maxHealth;
        healthSlider.fillAmount = sliderPercent;
    }

    void UpdateExp()
    {
        float sliderPercent = (float)GameManager.Instance.playerState.characterData.currentExp / GameManager.Instance.playerState.characterData.baseExp;
        expSlider.fillAmount = sliderPercent;
    }
}
