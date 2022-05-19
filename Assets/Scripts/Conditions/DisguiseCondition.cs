using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DisguiseCondition : MonoBehaviour, Condition
{
    [SerializeField] bool needDisguise = true;
    // Compare variable amount TO this amount
    public bool CheckCondition() {
        return PlayerManager.instance.GetDisguise() == needDisguise;
    }
}
