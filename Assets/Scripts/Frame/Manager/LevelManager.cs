using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    public static bool isGameOver = false;
    public Button restartButton;
    public string nextLevel;

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;

        if(restartButton != null)
        {
            restartButton.onClick.AddListener(RestartGame);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }

    public void LevelLost()
    {
        isGameOver = true;
        Invoke("LoadCurrentLevel", 2);
    }

    void LoadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RestartGame()
    {
        LoadCurrentLevel();
    }
}

