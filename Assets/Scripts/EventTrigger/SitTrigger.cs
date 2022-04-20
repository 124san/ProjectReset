using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitTrigger : EventTrigger
{
    [SerializeField] float sitTriggerTime = 2f;
    private float sitCountDown;
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
