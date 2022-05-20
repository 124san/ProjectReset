using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActivator : InteractableObject
{
    
    [SerializeField] bool turnTowardsPlayer = false;
    [SerializeField] protected DialogueData dialogueObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void HandleInteraction() {
        TurnTowardsPlayer();
        DialogueUI.instance.ShowDialogue(dialogueObject, gameObject);
        foreach(DialogueResponseEvents responseEvents in GetComponents<DialogueResponseEvents>()) {
            if (dialogueObject.Id == responseEvents.DialogueObject.Id) {
                if (dialogueObject.HasResponses) {
                    DialogueUI.instance.AddResponseEvents(responseEvents.Events);
                }
                else
                    DialogueUI.instance.AddResponseEvent(responseEvents.Events[0]);
                break;
            }
        }
    }

    protected void TurnTowardsPlayer() {
        if (turnTowardsPlayer) {
            transform.LookAt(PlayerManager.instance.transform);
        }
    }

    public void UpdateDialogueObject(DialogueData dialogueObject) {
        this.dialogueObject = dialogueObject;
    }
}
