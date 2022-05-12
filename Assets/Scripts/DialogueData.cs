using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "ScriptableObjects/Dialogue Data")]
public class DialogueData : ScriptableObject
{
    [SerializeField] int id;
    [SerializeField] Dialogue[] dialogues;
    [SerializeField] [TextArea] string[] dialogue;
    [SerializeField] Response[] responses;
    public Dialogue[] Dialogues => dialogues;
    public String[] DialogueOld => dialogue;
    public bool HasResponses => Responses != null && Responses.Length > 0;
    public Response[] Responses => responses;
    public int Id => id;
    
}
