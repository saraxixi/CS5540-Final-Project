using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : SingletonMono<SceneController>
{
    GameObject player;
    public GameObject playerPrefab;
    // NavMeshAgent playerAgent;
    public string nextScene;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }


    public void TransitionToDestination(TransitionPoint transitionPoint)
    {
        switch (transitionPoint.transitionType)
        {
            case TransitionPoint.TransitionType.SameScene:
                StartCoroutine(Transition(SceneManager.GetActiveScene().name, transitionPoint.destinationTag));
                break;
            case TransitionPoint.TransitionType.DifferentScene:
                break;
        }
    }

    IEnumerator Transition(string sceneName, TransitionDestination.DestinationTag destinationTag)
    { 
        //yield return SceneManager.LoadSceneAsync(sceneName);
        //yield return Instantiate(playerPrefab, GetDestination(destinationTag).transform.position, GetDestination(destinationTag).transform.rotation);

        player = GameManager.Instance.playerState.gameObject;
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.SetPositionAndRotation(GetDestination(destinationTag).transform.position, GetDestination(destinationTag).transform.rotation);
        player.GetComponent<CharacterController>().enabled = true;
        yield return null;
    }

    private TransitionDestination GetDestination(TransitionDestination.DestinationTag destinationTag)
    {
        var entrances = FindObjectsOfType<TransitionDestination>();
        for (int i = 0; i < entrances.Length; i++)
        {
            if (entrances[i].destinationTag == destinationTag)
            {
                return entrances[i];
            }
        }
        return null;
    }

    public void TransitionToNextLevel()
    { 
        StartCoroutine(LoadLevel(nextScene));
    }

    IEnumerator LoadLevel(string scene)
    {
        if (scene != "")
        {
            yield return SceneManager.LoadSceneAsync(scene);
            // yield return player = Instantiate(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);
            yield break;
        }

    }
}
