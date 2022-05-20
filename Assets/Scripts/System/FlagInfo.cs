using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FlagInfo : MonoBehaviour
{
    [SerializeField] public int id;
    [SerializeField] public bool value;

    public FlagInfo(int id, bool value) {
        this.id = id;
        this.value = value;
    }

    public void SetFlag() {
        FlowController.instance.SetFlag(id, value);
    }
}
