using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    [SerializeField] Transform inventoryUI;
    [SerializeField] GameObject slotPrefab;
    public bool isOpen;
    void Start()
    {
        InventorySystem.instance.onInventoryChangedEvent.AddListener(OnUpdateInventory);
        DrawInventory();
        isOpen = false;
        inventoryUI.gameObject.SetActive(false);
    }

    private void Update() {
        if (Input.GetButtonDown("Inventory")) {
            isOpen = !isOpen;
            inventoryUI.gameObject.SetActive(isOpen);
        }
    }

    void OnUpdateInventory() {
        foreach(Transform t in inventoryUI.transform) {
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
        obj.transform.SetParent(inventoryUI.transform, false);
        obj.GetComponent<ItemSlot>().correspondingItem = item.data;
        ItemSlot slot = obj.GetComponent<ItemSlot>();
        slot.Set(item);
    }
}
