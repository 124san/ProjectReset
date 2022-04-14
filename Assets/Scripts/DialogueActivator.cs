using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActivator : InteractableObject
{
    [SerializeField] DialogueData dialogueObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void HandleInteraction() {
        DialogueUI.instance.ShowDialogue(dialogueObject);
        if (TryGetComponent(out DialogueResponseEvents responseEvents)) {
            if (dialogueObject.HasResponses)
                DialogueUI.instance.AddResponseEvents(responseEvents.Events);
            else
                DialogueUI.instance.AddResponseEvent(responseEvents.Events[0]);
        }
    }
}