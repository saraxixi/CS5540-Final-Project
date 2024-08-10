using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tutorial", menuName = "Tutorial/Tutorail Data")]
public class TutorialData_SO : ScriptableObject
{
    public List<TutorialPiece> tutorialPieces = new List<TutorialPiece>();
}
