using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Response
{
    [SerializeField] private string responseText;
    [SerializeField] DialogueData dialogueObject;

    public string ResponseText => responseText;
    public DialogueData DialogueObject => dialogueObject;
}
