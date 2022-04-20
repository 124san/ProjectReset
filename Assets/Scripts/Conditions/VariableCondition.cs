using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VariableCondition : MonoBehaviour, Condition
{
    [SerializeField] int variableIndex = -1;
    [SerializeField] int amount = 1;
    [SerializeField] CompareMode mode;
    // Compare variable amount TO this amount
    public bool CheckCondition() {
        if (variableIndex < 0) return true;
        FlowController controller = FlowController.instance;
        switch (mode)
        {
            default: // Equal
                return amount == controller.GetVariable(variableIndex);
            case CompareMode.Bigger:
                return controller.GetVariable(variableIndex) > amount;
            case CompareMode.Beq:
                return controller.GetVariable(variableIndex) >= amount;
            case CompareMode.Smaller:
                return controller.GetVariable(variableIndex) < amount;
            case CompareMode.Seq:
                return controller.GetVariable(variableIndex) <= amount;
        }
    }
}
