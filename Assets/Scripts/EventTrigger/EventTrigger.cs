using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : MonoBehaviour
{
    // If isTriggered is true, events will not trigger even if all conditions are met
    public bool isTriggered;
    // If requireAllConditions is true, all conditions in the current GameObject must be true to trigger the event.
    // Otherwise, only one condition need to be satisfied to trigger the event
    [SerializeField] protected bool requireAllConditions = true;
    [SerializeField] protected UnityEvent thisEvent;

    public void SetTriggered(bool value) {
        isTriggered = value;
    }
}
