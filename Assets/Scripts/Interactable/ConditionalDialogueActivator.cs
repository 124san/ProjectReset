using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ConditionalDialogueActivator : DialogueActivator
{
    [SerializeField] DialogueData conditionalDialogue;
    private DialogueData activeDialogue;
    Condition[] conditions;

    private void Start() {
        conditions = GetComponents<Condition>();
    }
    public override void HandleInteraction() {
        activeDialogue = conditions.All(x => x.CheckCondition()) ? conditionalDialogue : dialogueObject;
        DialogueUI.instance.ShowDialogue(activeDialogue, gameObject);
        foreach(DialogueResponseEvents responseEvents in GetComponents<DialogueResponseEvents>()) {
            if (activeDialogue.Id == responseEvents.DialogueObject.Id) {
                if (activeDialogue.HasResponses) {
                    DialogueUI.instance.AddResponseEvents(responseEvents.Events);
                }
                else
                    DialogueUI.instance.AddResponseEvent(responseEvents.Events[0]);
                break;
            }
        }
    }

    public void UpdateConditionalDialogue(DialogueData dialogueObject) {
        this.conditionalDialogue = dialogueObject;
    }
}
