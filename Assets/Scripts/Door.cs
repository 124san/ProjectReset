using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    DoorEvent doorEvent;
    [SerializeField] InventoryItemData requiredKey;
    [SerializeField] bool consumesKey = true;
    [SerializeField] Transform pivot;
    [SerializeField] float rotateAngle = 90f;
    public bool isOpen = false;
    private Quaternion StartingRotation;
    private Vector3 RotateTo;
    void Start()
    {
        doorEvent = DoorEvent.instance;
        doorEvent.OnDoorEnterTrigger += OpenDoor; 
        doorEvent.OnDoorExitTrigger += CloseDoor;
        StartingRotation = pivot.rotation;
    }

    private void OpenDoor(Collider obj)
    {   
        // need key to open the door if requiredKey is present
        if (requiredKey) {
            InventorySystem inventory = InventorySystem.instance;
            if (inventory.Get(requiredKey) == null) {
                Debug.Log("You need keeeeeeeeeeey! "+requiredKey.displayName);
                return;
            }
            if (consumesKey) {
                inventory.Remove(requiredKey, 1);
            }
        }
        transform.RotateAround(pivot.position, pivot.up, rotateAngle);
        isOpen = true;
    }
    private void CloseDoor(Collider obj)
    {   
        transform.RotateAround(pivot.position, pivot.up, -rotateAngle);
        isOpen = false;
    }

    public void OpenDoorOneTimeUse() {
        OpenDoor(null);
        if (isOpen)
            gameObject.tag = "Obstacle";
    }
    
}
