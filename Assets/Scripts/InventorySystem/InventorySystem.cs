using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem instance;
    private Dictionary<InventoryItemData, InventoryItem> m_itemDictionary;
    public List<InventoryItem> inventory {get; private set;}

    public UnityEvent onInventoryChangedEvent;
    
    private void Awake() {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else {
            DontDestroyOnLoad(gameObject);
            instance = this;
            inventory = new List<InventoryItem>();
            m_itemDictionary = new Dictionary<InventoryItemData, InventoryItem>();
        }
    }
    public void OnReset () {
        // Go through all items and remove items that will be reset
        List<InventoryItem> toDelete = new List<InventoryItem>();
        foreach (var item in inventory) {
            if(!item.data.notResetting) {
                toDelete.Add(item);
            }
        }

        foreach (var item in toDelete) {
            item.RemoveFromStack(999);
            inventory.Remove(item);
            m_itemDictionary.Remove(item.data);
        }
    }

    public InventoryItem Get(InventoryItemData referenceData) {
        if(m_itemDictionary.TryGetValue(referenceData, out InventoryItem value)) {
            return value;
        }
        return null;
    }

    public void Add(InventoryItemData referenceData, int amount) {
        if(m_itemDictionary.TryGetValue(referenceData, out InventoryItem value)) {
            value.AddToStack(amount);
        }

        else {
            InventoryItem newItem = new InventoryItem(referenceData, amount);
            inventory.Add(newItem);
            m_itemDictionary.Add(referenceData, newItem);
        }
        onInventoryChangedEvent.Invoke();
    }

    public void Remove(InventoryItemData referenceData, int amount) {
        if(m_itemDictionary.TryGetValue(referenceData, out InventoryItem value)) {
            value.RemoveFromStack(amount);
            if (value.stackSize == 0) {
                inventory.Remove(value);
                m_itemDictionary.Remove(referenceData);
            }
        }
        onInventoryChangedEvent.Invoke();
    }

    [ContextMenu("Print Inventory")]
    void PrintInventory() {
        foreach (var referenceData in m_itemDictionary.Keys) {
            if(m_itemDictionary.TryGetValue(referenceData, out InventoryItem value)) {
                Debug.Log($"{value.data.displayName}: {value.stackSize}");
            }
        }
    }
}
