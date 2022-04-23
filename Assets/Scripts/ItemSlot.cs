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
    
    public void Set(InventoryItem item) {
        icon.sprite = item.data.icon;
        label.text = item.data.displayName;
        if (item.stackSize <=1 ) {
            stackObj.SetActive(false);
            return;
        }
        stackNumber.text = item.stackSize.ToString();
    }

    public void PlaceItem() {
        GridMovement playerMovement = PlayerManager.instance.GetComponent<GridMovement>();
        Transform playerTransform = PlayerManager.instance.transform;
        Vector3 placementPos = new Vector3(playerTransform.position.x, baseLevelHeight, playerTransform.position.z) + playerTransform.forward;
        // check if player is moving
        if (playerTransform.position == playerMovement.destination) {
            GameObject.Instantiate(correspondingItem.prefab, placementPos, Quaternion.Euler(0, 0, 0));
            InventorySystem.instance.Remove(correspondingItem, 1);
        }
    }
}
