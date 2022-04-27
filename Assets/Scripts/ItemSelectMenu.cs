using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelectMenu : MonoBehaviour
{
    InventoryItemData correspondingItem;
    [SerializeField] DialogueData noUseDialogue;

    public void Set(InventoryItemData item) {
        correspondingItem = item;
    }

    public void UseItem() {
        CloseItemSelectMenu();
        switch (correspondingItem.usage)
        {
            case ItemUsage.Droppable:
                PlaceItem();
                break;
            default:
                DialogueUI.instance.ShowDialogue(noUseDialogue);
                break;
        }
    }

    public void ShowItemInfo() {
        Debug.Log("pra");
        CloseItemSelectMenu();
        DialogueUI.instance.ShowDialogue(correspondingItem.infoDialogue);
    }

    void PlaceItem() {
        GridMovement playerMovement = PlayerManager.instance.GetComponent<GridMovement>();
        Transform playerTransform = PlayerManager.instance.transform;
        float baseLevelHeight = 1.5f;
        Vector3 placementPos = new Vector3(playerTransform.position.x, baseLevelHeight, playerTransform.position.z) + playerTransform.forward;
        // check if player is moving
        if (playerTransform.position == playerMovement.destination) {
            GameObject.Instantiate(correspondingItem.prefab, placementPos, Quaternion.Euler(0, 0, 0));
            InventorySystem.instance.Remove(correspondingItem, 1);
        }
    }

    public void CloseItemSelectMenu() {
        InventoryUIManager.instance.CloseItemSelectMenu();
    }
}
