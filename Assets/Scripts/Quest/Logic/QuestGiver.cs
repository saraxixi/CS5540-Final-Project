using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogueController))]

public class QuestGiver : MonoBehaviour
{
    DialogueController controller;
    QuestData_SO currentQuest;

    public DialogueData_SO startDialogue;
    public DialogueData_SO progressDialogue;
    public DialogueData_SO completeDialogue;
    public DialogueData_SO finishDialogue;

    #region Get Quest Status
    public bool IsStarted 
    { 
        get 
        {
            if (QuestManager.Instance.HaveQuest(currentQuest))
            {
                return QuestManager.Instance.GetTask(currentQuest).IsStarted;
            }
            return false;
        } 
    }

    public bool IsComplete
    {
        get
        {
            if (QuestManager.Instance.HaveQuest(currentQuest))
            {
                return QuestManager.Instance.GetTask(currentQuest).IsComplete;
            }
            return false;
        }
    }

    public bool IsFinished
    {
        get
        {
            if (QuestManager.Instance.HaveQuest(currentQuest))
            {
                return QuestManager.Instance.GetTask(currentQuest).IsFinished;
            }
            return false;
        }
    }

    #endregion


    void Awake()
    {
        controller = GetComponent<DialogueController>();
    }

    void Start()
    {
        controller.currentData = startDialogue;
        currentQuest = controller.currentData.GetQuest();
    }

    void Update()
    {
        if (IsStarted)
        {
            if (IsComplete)
            { 
                controller.currentData = completeDialogue;
            }
            else
            {
                controller.currentData = progressDialogue;
            }
        }

        if (IsFinished)
        { 
            controller.currentData = finishDialogue;
        }
    }
}
