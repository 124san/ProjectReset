using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{
    public InventoryItemData data {get; private set;}
    public int stackSize {get; private set;}

    public InventoryItem(InventoryItemData source, int amount) {
        data = source;
        AddToStack(amount);
    }

    public void AddToStack(int amount) {
        stackSize += amount;
    }

    public void RemoveFromStack(int amount) {
        stackSize -= amount;
        if (stackSize < 0) stackSize = 0;
    }
}
