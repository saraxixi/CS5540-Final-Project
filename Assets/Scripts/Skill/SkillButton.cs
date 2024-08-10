using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillButton : MonoBehaviour, IPointerClickHandler
{
    public SkillConfig skill;
    public void OnPointerClick(PointerEventData eventData)
    {
        SkillManager.Instance.activeSkill = skill;
        SkillManager.Instance.DisplaySkillInfo();
    }
}
