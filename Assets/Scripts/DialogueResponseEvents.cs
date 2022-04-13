using UnityEngine;
using System;

public class DialogueResponseEvents : MonoBehaviour
{
    [SerializeField] private DialogueData dialogueObject;
    [SerializeField] private ResponseEvent[] events;

    public ResponseEvent[] Events => events;

    public void OnValidate() {
        if (dialogueObject == null) return;
        if (dialogueObject.HasResponses) {
            if (events != null && events.Length == dialogueObject.Responses.Length) return;
            if (events == null) {
                events = new ResponseEvent[dialogueObject.Responses.Length];
            }
            else {
                Array.Resize(ref events, dialogueObject.Responses.Length);
            }
            for (int i = 0; i < dialogueObject.Responses.Length; i++) {
                Response response = dialogueObject.Responses[i];
                if (events[i] != null) {
                    events[i].name = response.ResponseText;
                    Debug.Log(events[i].name);
                    continue;
                }
            }   
        }
        else {
            if (events != null && events.Length == 1) return; 
            if (events == null) {
                events = new ResponseEvent[1];
            }
            else {
                Array.Resize(ref events, 1);
            }
        }
    }
}
