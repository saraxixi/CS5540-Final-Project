using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public DialogueData_SO currentData;
    bool canTalk = false;
    MouseManager mouseManager;

    private void Awake()
    {
        mouseManager = FindObjectOfType<MouseManager>();
    }

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
            OpenDialogue();
        }

    }

    void OpenDialogue()
    {
        // Open dialogue UI
        // Pass the dialogue data to UI
        mouseManager.UnlockMouse();
        DialogueUI.Instance.UpdateDialogueData(currentData);
        DialogueUI.Instance.UpdateMainDialogue(currentData.dialoguePieces[0]);
    }
}
