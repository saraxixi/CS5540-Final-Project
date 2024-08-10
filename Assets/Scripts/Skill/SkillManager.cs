using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : SingletonMono<SkillManager>
{
    public SkillConfig activeSkill;
    [Header("UI Panel")]
    public GameObject skillTreePanel;
    public GameObject skillDescriptionPanel;

    [Header("UI")]
    public Image skillImage;
    public Text skillName, skillLevel, skillDescription;

    [Header("Skill Point")]
    private int skillPoint;

    public SkillButton[] skillButtons;

    public Text pointText;
    private bool isOpen;

    void Start()
    {   
        UpdatePointUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            OpenAndCloseUI();
        }
        UpdatePointUI();
    }

    public void UpGradeButton()
    {
        if (activeSkill == null)
            return;

        if (skillPoint > 0 && activeSkill.preSkills.Length == 0)
        {
            UpGradeSkill();
        }

        if (skillPoint > 0)
        { 
            for (int i = 0; i < activeSkill.preSkills.Length; i++)
            {
                if (activeSkill.preSkills[i].IsUnlock == true)
                {
                    UpGradeSkill();
                    break;
                }
                    
            }
        }
    }

    public void UpGradeSkill()
    {
        skillButtons[activeSkill.SkillID].transform.GetChild(0).GetChild(0).GetComponent<Image>().color = Color.white;
        skillButtons[activeSkill.SkillID].transform.GetChild(1).gameObject.SetActive(true);
        activeSkill.SkillLevel++;
        skillButtons[activeSkill.SkillID].transform.GetChild(1).GetChild(0).GetComponent<Text>().text = activeSkill.SkillLevel.ToString("00");

        GameManager.Instance.playerState.characterData.currentSkillPoint--;
        DisplaySkillInfo();
        UpdatePointUI();

        activeSkill.IsUnlock = true;
    }

    public void DisplaySkillInfo()
    {
        if (activeSkill == null)
        { 
            skillImage.gameObject.SetActive(false);
            skillName.gameObject.SetActive(false);
            skillLevel.gameObject.SetActive(false);
            skillDescription.gameObject.SetActive(false);
        }
        else
        {
            skillImage.gameObject.SetActive(true);
            skillName.gameObject.SetActive(true);
            skillLevel.gameObject.SetActive(true);
            skillDescription.gameObject.SetActive(true);

            skillImage.sprite = activeSkill.SkillIcon;
            skillName.text = activeSkill.SkillName;
            skillLevel.text = activeSkill.SkillLevel.ToString("00");
            skillDescription.text = activeSkill.SkillDescription;

        }

    }

    private void UpdatePointUI()
    {
        skillPoint = GameManager.Instance.playerState.characterData.currentSkillPoint;
        pointText.text = "POINTS: " + skillPoint.ToString("00");
    }

    public void OpenAndCloseUI()
    {
        isOpen = !isOpen;
        skillTreePanel.SetActive(isOpen);
        skillDescriptionPanel.SetActive(isOpen);
        DisplaySkillInfo();
    }
}
