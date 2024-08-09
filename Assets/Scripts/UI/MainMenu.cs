using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    Button newGameButton;
    Button continueButton;
    Button quitButton;

    private void Awake()
    {
        newGameButton = transform.GetChild(1).GetComponent<Button>();
        continueButton = transform.GetChild(2).GetComponent<Button>();
        quitButton = transform.GetChild(3).GetComponent<Button>();

        newGameButton.onClick.AddListener(NewGame);
        continueButton.onClick.AddListener(ContinueGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    void NewGame()
    { 
        PlayerPrefs.DeleteAll();
        SceneController.Instance.TransitionToFirstLevel();

    }

    void ContinueGame()
    { 
        // Change Scene and Load Data
    }

    void QuitGame()
    { 
        Application.Quit();
        Debug.Log("Quit Game");
    }
}
