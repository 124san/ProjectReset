using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AutoFlagEventTrigger : EventTrigger
{
    Condition[] conditions;
    void Start() {
        conditions = GetComponents<Condition>();
    }
    void Update()
    {
        if (!isTriggered && ConditionHelper.CheckCondition(requireAllConditions, conditions)) {
            thisEvent?.Invoke();
            isTriggered = true;
        }
        
    }
}
