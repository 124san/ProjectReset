using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExitTrigger : EventTrigger
{
    Condition[] conditions;
    
    private void OnTriggerExit(Collider other) {
        if (!other.CompareTag("Player")) return;
        conditions = GetComponents<Condition>();
        if (ConditionHelper.CheckCondition(requireAllConditions, conditions)) {
            thisEvent?.Invoke();
            isTriggered = true;
        }
    }
}
