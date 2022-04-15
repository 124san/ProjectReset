using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class VariableInfo : MonoBehaviour
{
    [SerializeField] public int id;
    [SerializeField] public int value;

    public VariableInfo(int id, int value) {
        this.id = id;
        this.value = value;
    }

    public void SetVariable() {
        FlowController.instance.SetVariable(id, value);
    }
}
