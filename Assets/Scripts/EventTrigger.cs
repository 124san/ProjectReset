using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : MonoBehaviour
{
    [SerializeField] UnityEvent thisEvent;
    public void InvokeEvent() {
        thisEvent?.Invoke();
    }
}
