using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/Dialogue Data")]
public class DialogueData_SO : ScriptableObject
{
    public List<DialoguePiece> dialoguePieces = new List<DialoguePiece>();
    public Dictionary<string, DialoguePiece> dialogueIndex = new Dictionary<string, DialoguePiece>();

#if UNITY_EDITOR
    void OnValidate()
    {
        dialogueIndex.Clear();
        foreach (var piece in dialoguePieces)
        {
            if (!dialogueIndex.ContainsKey(piece.ID))
            {
                dialogueIndex.Add(piece.ID, piece);
            }
        }
    }
#else
    void Awake()
    {
        dialogueIndex.Clear();
        foreach (var piece in dialoguePieces)
        {
            if (!dialogueIndex.ContainsKey(piece.ID))
            {
                dialogueIndex.Add(piece.ID, piece);
            }
        }
    }
#endif

    public QuestData_SO GetQuest()
    {
        QuestData_SO currentQuest = null;

        foreach (var piece in dialoguePieces)
        {
            if (piece.quest != null)
            {
                currentQuest = piece.quest;
            }
        }
        return currentQuest;
    }
}