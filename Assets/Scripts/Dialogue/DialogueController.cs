using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public DialogueData_SO currentData;
    bool canTalk = false;
    public Player_Controller player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && currentData != null)
        {
            canTalk = true;
        }
    }
    
    void Update()
    {
        if (canTalk && Input.GetKeyDown(KeyCode.F))
        {
            Cursor.lockState = CursorLockMode.None;
            player.isInDialogue = true;
            OpenDialogue();
        }

    }

    void OpenDialogue()
    { 
        // Open dialogue UI
        // Pass the dialogue data to UI
        DialogueUI.Instance.UpdateDialogueData(currentData);
        DialogueUI.Instance.UpdateMainDialogue(currentData.dialoguePieces[0]);
    }
}
