using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManager : SingletonMono<GameManager>
{

    public static bool isGameOver = false;
    public CharacterState playerState;
    public string nextScene;
    private CinemachineFreeLook followCamera;

    override protected void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        isGameOver = false;
    }

    void Update()
    {
        if (isGameOver)
        {
            return;
        }

        if (GameManager.Instance.playerState.currentHealth == 0)
        {
            LevelLost();
        }
    }

    public void RegisterPlayer(CharacterState player)
    { 
        playerState = player;
        followCamera = FindObjectOfType<CinemachineFreeLook>();

        if (followCamera != null)
        { 
            followCamera.Follow = playerState.transform.GetChild(2);
            followCamera.LookAt = playerState.transform.GetChild(2);
        }
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(nextScene);
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

    public Transform GetEntrance()
    {
        foreach (var item in FindObjectsOfType<TransitionDestination>())
        {
            if (item.destinationTag == TransitionDestination.DestinationTag.ENTER)
            { 
                return item.transform;
            }
        }
        return null;
    }
}

