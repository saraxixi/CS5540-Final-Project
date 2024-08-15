using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    void Start()
    {
        videoPlayer.loopPointReached += EndPlay;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

public void EndPlay(VideoPlayer videoPlayer) 
    { 
        SceneController.Instance.TransitionToNextLevel("Scene_1");
    }
}
