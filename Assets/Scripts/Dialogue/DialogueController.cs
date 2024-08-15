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
            TutorialManager.Instance.tutorialPanel.SetActive(true);
            TutorialManager.Instance.SetText("Press [F] to talk");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DialogueUI.Instance.dialoguePanel.SetActive(false);
            canTalk = false;
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
