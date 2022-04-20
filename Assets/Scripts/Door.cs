using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public DoorEvent doorEvent;
    public InventoryItemData requiredKey;
    [SerializeField] bool consumesKey = true;
    [SerializeField] int requiredFlag = -1;
    [SerializeField] Transform pivot;
    [SerializeField] float rotateAngle = 90f;
    public bool isOpen = false;
    private Quaternion StartingRotation;
    private Vector3 RotateTo;
    void Start()
    {
        // doorEvent = this.GetComponentInChildren<DoorEvent>();
        // if (doorEvent)
        // {
        //     doorEvent.OnDoorEnterTrigger += OpenDoor; 
        //     doorEvent.OnDoorExitTrigger += CloseDoor;
        // }
        StartingRotation = pivot.rotation;
    }
    // Check if player can open the door with current condition
    public bool CanOpen() {
        if (requiredKey && InventorySystem.instance.Get(requiredKey) == null) {
            return false;
        }
        if (requiredFlag >= 0 && !FlowController.instance.GetFlag(requiredFlag)) {
            return false;
        }
        return true;
    }
    public void OpenDoor()
    {   
        if (consumesKey) {
            InventorySystem.instance.Remove(requiredKey, 1);
        }
        transform.RotateAround(pivot.position, pivot.up, rotateAngle);
        isOpen = true;
    }
    public void CloseDoor()
    {   
        transform.RotateAround(pivot.position, pivot.up, -rotateAngle);
        isOpen = false;
    }
    
}
