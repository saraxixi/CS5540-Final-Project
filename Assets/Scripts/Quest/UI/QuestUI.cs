using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : SingletonMono<QuestUI>
{
    [Header("Elements")]
    public GameObject questPanel;
    // public ItemToolTip toolTip;
    bool isOpen;

    [Header("Quest Name")]
    public RectTransform questListTransform;
    public QuestNameButton questNameButton;

    [Header("Text Content")]
    public Text questContentText;

    [Header("Requirement")]
    public RectTransform requireTransform;
    public QuestRequirement requirement;

    // [Header("Reward Panel")]
    // public RectTransform rewardTransform;
    // public ItemUI rewardUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            isOpen = !isOpen;
            questPanel.SetActive(isOpen);
            questContentText.text = "";

            // Show the quest panel
            SetupQuestList();
        }
    }

    public void SetupQuestList()
    {
        foreach (Transform item in questListTransform)
        {
            Destroy(item.gameObject);
        }

        //foreach (Transform item in rewardTransform)
        //{
        //    Destroy(item.gameObject);
        //}

        foreach (Transform item in requireTransform)
        {
            Destroy(item.gameObject);
        }

        foreach (var task in QuestManager.Instance.tasks)
        {
            var newTask = Instantiate(questNameButton, questListTransform);
            newTask.SetupNameButton(task.questData);
            newTask.questContentText = questContentText;
        }
    }

    public void SetupRequireList(QuestData_SO questData)
    {
        foreach (Transform item in requireTransform)
        {
            Destroy(item.gameObject);
        }

        foreach (var require in questData.questRequires)
        {
            var q = Instantiate(requirement, requireTransform);
            q.SetupRequirement(require.name, require.requireAmount, require.currentAmount);
        }
    }
}
