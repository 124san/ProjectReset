using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryUIManager : MonoBehaviour
{
    public static InventoryUIManager instance;
    public bool isItemSelectMenuOpen;
    public GameObject itemSelectMenu;
    [SerializeField] GameObject itemSelectMenuPrefab;
    [SerializeField] Transform inventoryUI;
    [SerializeField] GameObject slotPrefab;
    public bool isOpen;
    void Awake() {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else {
            instance = this;
        }
    }
    void Start()
    {
        InventorySystem.instance.onInventoryChangedEvent.AddListener(OnUpdateInventory);
        DrawInventory();
        isOpen = false;
        inventoryUI.gameObject.SetActive(false);
    }

    private void Update() {
        if (Input.GetButtonDown("Inventory")) {
            ToggleInventoryUI();
        }
        if (Input.GetMouseButtonDown(0) && isItemSelectMenuOpen && EventSystem.current.currentSelectedGameObject == null) {
            CloseItemSelectMenu();
        }
    }

    void ToggleInventoryUI() {
        if (isOpen) {
            CloseItemSelectMenu();
        }
        isOpen = !isOpen;
        inventoryUI.gameObject.SetActive(isOpen);
    }

    void OnUpdateInventory() {
        foreach(Transform t in inventoryUI.transform) {
            Destroy(t.gameObject);
        }

        DrawInventory();
    }

    public void DrawInventory() {
        CloseItemSelectMenu();
        foreach(InventoryItem item in InventorySystem.instance.inventory) {
            AddInventorySlot(item);
        }
    }

    public void OpenItemSelectMenu(ItemSlot itemSlot) {
        CloseItemSelectMenu();
        isItemSelectMenuOpen = true;
        itemSelectMenu = Instantiate(itemSelectMenuPrefab);
        RectTransform itemSelectRect = itemSelectMenu.GetComponent<RectTransform>();
        itemSelectRect.SetParent(itemSlot.transform, false);
        itemSelectRect.anchoredPosition = new Vector2(itemSlot.GetComponent<RectTransform>().sizeDelta.x/2, -itemSlot.GetComponent<RectTransform>().sizeDelta.y/2);
        itemSelectRect.SetParent(this.transform, true);
        itemSelectMenu.GetComponent<ItemSelectMenu>().Set(itemSlot.correspondingItem);
    }

    public void CloseItemSelectMenu() {
        isItemSelectMenuOpen = false;
        Destroy(itemSelectMenu);
    }

    public void AddInventorySlot(InventoryItem item) {
        GameObject obj = Instantiate(slotPrefab);
        obj.transform.SetParent(inventoryUI.transform, false);
        obj.GetComponent<ItemSlot>().correspondingItem = item.data;
        ItemSlot slot = obj.GetComponent<ItemSlot>();
        slot.Set(item);
    }
}
