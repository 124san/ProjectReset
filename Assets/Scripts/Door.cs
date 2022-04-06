using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Doorevent doorEvent;
    private Quaternion StartingRotation;
    private Vector3 RotateTo;
    void Start()
    {
         doorEvent.OnDoorEnterTrigger += DoorEvent_OnDoorEnterTrigger; 
         doorEvent.OnDoorExitTrigger += DoorEvent_OnDoorExitTrigger;

         StartingRotation = gameObject.transform.rotation;
         RotateTo = new Vector3(0, 90, 0);
    }

    private void DoorEvent_OnDoorEnterTrigger(Collider obj)
    {   
        gameObject.transform.rotation = Quaternion.Euler(RotateTo);
    }
    private void DoorEvent_OnDoorExitTrigger(Collider obj)
    {   
        gameObject.transform.rotation = StartingRotation;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
        DoorEvent_OnDoorEnterTrigger(null);    
        }  
    }
}
