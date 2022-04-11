using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Dialogue Data")]
public class DialogueData : ScriptableObject
{
    [SerializeField] [TextArea] string[] dialogue;
    [SerializeField] Response[] responses;
    public string[] Dialogue => dialogue;
    public bool HasResponses => Responses != null && Responses.Length > 0;
    public Response[] Responses => responses;
}
