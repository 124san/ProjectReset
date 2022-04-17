using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitTrigger : MonoBehaviour
{
    [SerializeField] float sitTriggerTime = 2f;
    private float sitCountDown;
    public bool isTriggered = false;
    GridMovement player;
    public void SetTrigger(bool value) {
        isTriggered = value;
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<GridMovement>();
        sitCountDown = sitTriggerTime;
    }

    void Update()
    {
        if (isTriggered) return;
        if (player.isSitting) {
            Debug.Log(sitCountDown);
            if (sitCountDown <= 0f) {
                EventTrigger thisEvent = GetComponent<EventTrigger>();
                
                Debug.Log(thisEvent);
                thisEvent?.InvokeEvent();
                isTriggered = true;
            }
            else sitCountDown -= Time.deltaTime;
        }
        else {
            sitCountDown = sitTriggerTime;
        }
    }
}
