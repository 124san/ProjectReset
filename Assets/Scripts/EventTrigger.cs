using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : MonoBehaviour
{
    public bool isTriggered;
    [SerializeField] protected bool requireAllConditions = true;
    [SerializeField] protected UnityEvent thisEvent;

    public void SetTriggered(bool value) {
        isTriggered = value;
    }
}
