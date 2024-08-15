using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionPoint : MonoBehaviour
{
    public enum TransitionType
    {
        SameScene,
        DifferentScene
    }

    // public GameObject portal;

    [Header("Transition Info")]
    public string sceneName;
    public GameObject portal;
    public TransitionType transitionType;
    public TransitionDestination.DestinationTag destinationTag;
    public int transitionLimit;

    private bool canTransit;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && canTransit && transitionType == TransitionType.SameScene)
        {
            portal.SetActive(false);
            // SceneController.Instance.TransitionToDestination(this);
        }
        else if (Input.GetKeyDown(KeyCode.F) && canTransit && transitionType == TransitionType.DifferentScene)
        {
            SceneController.Instance.TransitionToDestination(this);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.Instance.playerState.characterData.currentLevel >= transitionLimit)
        { 
            TutorialManager.Instance.SetText("Press [F] to enter");
            canTransit = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canTransit = false;
        }
    }
}
