using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogueController))]

public class QuestGiver : MonoBehaviour
{
    DialogueController dialogueController;
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
        dialogueController = GetComponent<DialogueController>();
    }

    void Start()
    {
        dialogueController.currentData = startDialogue;
        currentQuest = dialogueController.currentData.GetQuest();
    }

    void Update()
    {
        if (IsStarted)
        {
            if (IsComplete)
            { 
                dialogueController.currentData = completeDialogue;
            }
            else
            {
                dialogueController.currentData = progressDialogue;
            }
        }

        if (IsFinished)
        { 
            dialogueController.currentData = finishDialogue;
        }
    }
}
