using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FlagCondition : MonoBehaviour, Condition
{
    [SerializeField] int flagIndex = -1;
    [SerializeField] bool value = true;
    // Compare variable amount TO this amount
    public bool CheckCondition() {
        return FlowController.instance.GetFlag(flagIndex) == value;
    }
}
