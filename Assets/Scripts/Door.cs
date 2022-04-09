using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableObject
{
    public DoorEvent doorEvent;
    [SerializeField] bool consumesKey = true;
    [SerializeField] Transform pivot;
    [SerializeField] float rotateAngle = 90f;
    public bool isOpen = false;
    private Quaternion StartingRotation;
    private Vector3 RotateTo;
    void Start()
    {
        doorEvent = this.GetComponentInChildren<DoorEvent>();
        if (doorEvent)
        {
            doorEvent.OnDoorEnterTrigger += OpenDoor; 
            doorEvent.OnDoorExitTrigger += CloseDoor;
        }
        StartingRotation = pivot.rotation;
    }

    private void OpenDoor(Collider obj)
    {   
        // need key to open the door if requiredKey is present
        if (referenceItem) {
            InventorySystem inventory = InventorySystem.instance;
            if (inventory.Get(referenceItem) == null) {
                Debug.Log("You need keeeeeeeeeeey! "+referenceItem.displayName);
                return;
            }
            if (consumesKey) {
                inventory.Remove(referenceItem, referenceAmount);
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

    public override void HandleInteraction() {
        OpenDoor(null);
    }
    
}
