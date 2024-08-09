using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : SingletonMono<SceneController>
{

    public void TransitionToFirstLevel()
    { 
        StartCoroutine(LoadLevel("scene_1"));
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
