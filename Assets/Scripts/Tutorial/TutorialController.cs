using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public TutorialData_SO startQuestTutorial;
    private bool isStartTutorialStarted = false;
    private int startTutorialIndex = 0;

    public TutorialData_SO finishQuestTutorial;
    private bool isFinishTutorialStarted = false;
    private int finishTutorialIndex = 0;

    DialogueController dialogueController;
    QuestData_SO currentQuest;
    


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
        currentQuest = dialogueController.currentData.GetQuest();
    }


    private void Update()
    {
        if (IsStarted && !isStartTutorialStarted)
        {
            isStartTutorialStarted = true;
            TutorialManager.Instance.SetTutorialData(startQuestTutorial, startTutorialIndex);
        }

        if (isStartTutorialStarted)
        {
            UpdateStartTutorial(startTutorialIndex);
        }

        if (IsFinished && !isFinishTutorialStarted)
        {
            isFinishTutorialStarted = true;
            TutorialManager.Instance.SetTutorialData(finishQuestTutorial, finishTutorialIndex);
        }

        if (isFinishTutorialStarted)
        {
            UpdateFinishTutorial(finishTutorialIndex);
        }
    }

    public void UpdateStartTutorial(int currentPieceIndex)
    {
        if (TutorialManager.Instance.gameObject.activeSelf)
        {
            if (currentPieceIndex == 0 && Input.GetKeyDown(KeyCode.I))
            {
                currentPieceIndex++;
                TutorialManager.Instance.NextPiece(currentPieceIndex);
            }
            else if (currentPieceIndex == 1 && Input.GetKeyDown(KeyCode.N))
            {
                currentPieceIndex++;
                TutorialManager.Instance.NextPiece(currentPieceIndex);
            }
        }
    }

    public void UpdateFinishTutorial(int currentPieceIndex)
    {
        if (TutorialManager.Instance.gameObject.activeSelf)
        {
            if (currentPieceIndex == 0 && Input.GetKeyDown(KeyCode.B))
            {
                currentPieceIndex++;
                TutorialManager.Instance.NextPiece(currentPieceIndex);
            }
            else if (currentPieceIndex == 1 && Input.GetKeyDown(KeyCode.N))
            { 
                currentPieceIndex++;
                TutorialManager.Instance.NextPiece(currentPieceIndex);
            }
        }
    }
}
