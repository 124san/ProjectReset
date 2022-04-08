using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public InventoryItemData referenceItem;
    public int pickupAmount = 1;
    public int noResetFlag = -1;

    void Awake() {
        if (noResetFlag >= 0 && NoResetManager.instance && NoResetManager.instance.flags[noResetFlag]) {
            Destroy(gameObject);
        }
    }
    
    public void OnHandlePickupItem() {
        InventorySystem.instance.Add(referenceItem, pickupAmount);
        if (noResetFlag >= 0 && NoResetManager.instance) {
            NoResetManager.instance.flags[noResetFlag] = true;
        }
        Destroy(gameObject);
    }
}
