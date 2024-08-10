using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class LevelManager : SingletonMono<LevelManager>
{
    public TutorialData_SO level1Tutoral;
    int level1Index = 0;
    bool isLevel1TutorialStarted = false;

    public TutorialData_SO level2Tutoral;
    int level2Index = 0;
    bool isLevel2TutorialStarted = false;

    void Update()
    {
        if (GameManager.Instance.playerState.characterData.currentLevel == 1 && !isLevel1TutorialStarted)
        {
            isLevel1TutorialStarted = true;
            TutorialManager.Instance.SetTutorialData(level1Tutoral, level1Index);
        }

        if (isLevel1TutorialStarted)
        {
            UpdateLevel1Tutorial();
        }

        if (GameManager.Instance.playerState.characterData.currentLevel == 2 && !isLevel2TutorialStarted)
        { 
            isLevel2TutorialStarted = true;
            TutorialManager.Instance.SetTutorialData(level2Tutoral, level2Index);
        }

        if (isLevel2TutorialStarted)
        {
            UpdateLevel2Tutorial();
        }
    }

    private void UpdateLevel1Tutorial()
    {
        if (TutorialManager.Instance.gameObject.activeSelf)
        {
            if (level1Index == 0 && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) ||
                Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)))
            {
                level1Index++;
                TutorialManager.Instance.NextPiece(level1Index);
            }
            else if (level1Index == 1 && Input.GetKeyDown(KeyCode.Space))
            {
                level1Index++;
                TutorialManager.Instance.NextPiece(level1Index);
            }
            else if (level1Index == 2 && Input.GetMouseButtonDown(0))
            {
                level1Index++;
                TutorialManager.Instance.NextPiece(level1Index);
            }
            else if (level1Index == 3 && Input.GetKeyDown(KeyCode.N))
            {
                level1Index++;
                TutorialManager.Instance.NextPiece(level1Index);
            }
        }
    }

    private void UpdateLevel2Tutorial()
    {
        if (TutorialManager.Instance.gameObject.activeSelf)
        {
            if (level1Index == 0 && Input.GetKeyDown(KeyCode.K))
            {
                level1Index++;
                TutorialManager.Instance.NextPiece(level1Index);
            }
            else if (level1Index == 1 && Input.GetMouseButtonDown(0))
            {
                level1Index++;
                TutorialManager.Instance.NextPiece(level1Index);
            }
            else if (level1Index == 2 && Input.GetMouseButtonDown(0))
            {
                level1Index++;
                TutorialManager.Instance.NextPiece(level1Index);
            }
            else if (level1Index == 3 && Input.GetKeyDown(KeyCode.Q))
            {
                level1Index++;
                TutorialManager.Instance.NextPiece(level1Index);
            }
        }
    }
}

