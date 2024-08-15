using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : SingletonMono<TutorialManager>
{
    public TutorialData_SO currentTutorial;
    private TutorialPiece currentPiece;
    public GameObject tutorialPanel;
    public Text tutorialText;

    public void NextPiece(int currentPieceIndex)
    {
        if (currentPieceIndex < currentTutorial.tutorialPieces.Count)
        {
            currentPiece = currentTutorial.tutorialPieces[currentPieceIndex];
            tutorialText.text = currentPiece.text;
        }
        else
        {
            tutorialPanel.SetActive(false);
        }
    }

    public void SetTutorialData(TutorialData_SO newTutorialData, int currentPieceIndex)
    {
        currentTutorial = newTutorialData;
        currentPiece = currentTutorial.tutorialPieces[currentPieceIndex];
        tutorialText.text = currentPiece.text;
        tutorialPanel.SetActive(true);
    }

    public void SetText(string text)
    {
        tutorialPanel.SetActive(true);
        tutorialText.text = text;
    }
}
