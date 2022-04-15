using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    // Start is called before the first frame update
    public InventoryItemData referenceItem;
    public int referenceAmount = 1;
    public int noResetFlag = -1;
    void Awake() {
        if (noResetFlag >= 0 && FlowController.instance && FlowController.instance.flags[noResetFlag]) {
            Destroy(gameObject);
        }
    }
    public abstract void HandleInteraction();
    public void AddReferenceItem() {
        InventorySystem.instance.Add(referenceItem, referenceAmount);
    }
}
