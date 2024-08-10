using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : SingletonMono<GameManager>
{

    public static bool isGameOver = false;
    public CharacterState playerState;
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

    public void RegisterPlayer(CharacterState player)
    { 
        playerState = player;
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

