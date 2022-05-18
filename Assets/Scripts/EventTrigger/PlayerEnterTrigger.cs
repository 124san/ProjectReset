using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnterTrigger : EventTrigger
{
    Condition[] conditions;
    
    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Player")) return;
        conditions = GetComponents<Condition>();
        if (ConditionHelper.CheckCondition(requireAllConditions, conditions)) {
            thisEvent?.Invoke();
            isTriggered = true;
        }
    }

}
