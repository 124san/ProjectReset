using UnityEngine;
using System;
/*
 * This class handles events after a dialogue is finished.
 *
*/
public class DialogueResponseEvents : MonoBehaviour
{
    // Corresponding dialogue to this event, must match the current dialogue for event to trigger
    [SerializeField] private DialogueData dialogueObject;
    // Events that will be triggered based on what response is selected
    // Length of events must match the number of responses
    [SerializeField] private ResponseEvent[] events;

    public DialogueData DialogueObject => dialogueObject;
    public ResponseEvent[] Events => events;

    // Validate length of events to number of responses
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
