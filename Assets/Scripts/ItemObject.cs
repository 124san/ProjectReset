using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : InteractableObject
{
    public override void HandleInteraction() {
        AddReferenceItem();
        if (noResetFlag >= 0 && FlowController.instance) {
            FlowController.instance.SetFlag(noResetFlag, true);
        }
        Destroy(gameObject);
    }
}
