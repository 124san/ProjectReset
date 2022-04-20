using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StartEventTrigger : EventTrigger
{
    Condition[] conditions;
    
    void Start()
    {
        conditions = GetComponents<Condition>();
        if (ConditionHelper.CheckCondition(requireAllConditions, conditions)) {
            thisEvent?.Invoke();
            isTriggered = true;
        }
        
    }
}
