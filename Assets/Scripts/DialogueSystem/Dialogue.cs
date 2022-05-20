using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Dialogue
{
    public DialogueBubbleTarget bubbleTarget;
    public string speakerName;
    [TextArea] public string dialogueText;
}
