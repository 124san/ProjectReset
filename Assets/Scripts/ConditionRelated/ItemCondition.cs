using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemCondition : MonoBehaviour, Condition
{
    [SerializeField] InventoryItemData requiredItem;
    [SerializeField] int amount = 1;
    [SerializeField] CompareMode mode;
    public bool CheckCondition() {
        if (requiredItem == null) return true;
        InventorySystem inventory = InventorySystem.instance;
        bool hasItem = inventory.Get(requiredItem) != null;
        switch (mode)
        {
            default: // Equal
                if (amount == 0) {
                    return !hasItem;
                }
                return hasItem && inventory.Get(requiredItem).stackSize == amount;
            case CompareMode.Bigger:
                return hasItem && inventory.Get(requiredItem).stackSize > amount;
            case CompareMode.Beq:
                return hasItem && inventory.Get(requiredItem).stackSize >= amount;
            case CompareMode.Smaller:
                return !hasItem || (hasItem && inventory.Get(requiredItem).stackSize < amount);
            case CompareMode.Seq:
                return !hasItem || (hasItem && inventory.Get(requiredItem).stackSize <= amount);
        }
    }
}
