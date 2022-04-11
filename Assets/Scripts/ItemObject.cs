using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : InteractableObject
{
    public override void HandleInteraction() {
        InventorySystem.instance.Add(referenceItem, referenceAmount);
        if (noResetFlag >= 0 && FlowController.instance) {
            FlowController.instance.flags[noResetFlag] = true;
        }
        Destroy(gameObject);
    }
}
