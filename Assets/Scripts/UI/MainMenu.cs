using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button newGameButton;
    public Button continueButton;
    public Button quitButton;

    private void Awake()
    {
        newGameButton.onClick.AddListener(NewGame);
        continueButton.onClick.AddListener(ContinueGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    void NewGame()
    { 
        PlayerPrefs.DeleteAll();
        SceneController.Instance.TransitionToNextLevel("Scene_StartCG");

    }

    void ContinueGame()
    {
        SceneController.Instance.TransitionToNextLevel("Scene_1");
    }

    void QuitGame()
    { 
        Application.Quit();
        Debug.Log("Quit Game");
    }
}
