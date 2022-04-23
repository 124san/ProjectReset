using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    [SerializeField] GameObject slotPrefab;
    void Start()
    {
        InventorySystem.instance.onInventoryChangedEvent.AddListener(OnUpdateInventory);
        DrawInventory();
    }

    void OnUpdateInventory() {
        foreach(Transform t in transform) {
            Destroy(t.gameObject);
        }

        DrawInventory();
    }

    public void DrawInventory() {
        foreach(InventoryItem item in InventorySystem.instance.inventory) {
            AddInventorySlot(item);
        }
    }

    public void AddInventorySlot(InventoryItem item) {
        GameObject obj = Instantiate(slotPrefab);
        obj.transform.SetParent(transform, false);
        obj.GetComponent<ItemSlot>().correspondingItem = item.data;
        ItemSlot slot = obj.GetComponent<ItemSlot>();
        slot.Set(item);
    }
}
