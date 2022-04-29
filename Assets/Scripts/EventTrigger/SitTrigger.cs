using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitTrigger : EventTrigger
{
    [SerializeField] float sitTriggerTime = 2f;
    private float sitCountDown;
    GridMovement player;
    void Start()
    {
        sitCountDown = sitTriggerTime;
    }

    void Update()
    {
        if (isTriggered) return;
        player = PlayerManager.instance.GetComponent<GridMovement>();
        if (player.isSitting) {
            if (sitCountDown <= 0f) {
                thisEvent?.Invoke();
                isTriggered = true;
            }
            else sitCountDown -= Time.deltaTime;
        }
        else {
            sitCountDown = sitTriggerTime;
        }
    }
}
