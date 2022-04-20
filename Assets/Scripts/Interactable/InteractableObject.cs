using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    // Start is called before the first frame update
    public InventoryItemData referenceItem;
    // Player can interact with InteractableObject only if isInteractable is set to true
    public bool isInteractable = true;
    public int referenceAmount = 1;
    public int noResetFlag = -1;
    void Awake() {
        if (noResetFlag >= 0 && FlowController.instance && FlowController.instance.GetFlag(noResetFlag)) {
            Destroy(gameObject);
        }
    }
    public abstract void HandleInteraction();
    public void AddReferenceItem() {
        InventorySystem.instance.Add(referenceItem, referenceAmount);
    }
    public void SetInteractable(bool value) {
        isInteractable = value;
    }
}
