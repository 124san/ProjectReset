using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] Text label;
    [SerializeField] GameObject stackObj;
    [SerializeField] Text stackNumber;
    public InventoryItemData correspondingItem;
    private float baseLevelHeight = 1.5f;

    private void Awake() {
    }
    
    public void Set(InventoryItem item) {
        icon.sprite = item.data.icon;
        label.text = item.data.displayName;
        if (item.stackSize <=1 ) {
            stackObj.SetActive(false);
            return;
        }
        stackNumber.text = item.stackSize.ToString();
    }

    public void OpenItemMenu() {
        Debug.Log("openitemmenu");
        InventoryUIManager.instance.OpenItemSelectMenu(this);
    }

}
