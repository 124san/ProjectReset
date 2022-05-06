using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivot : MonoBehaviour
{
    public static CameraPivot instance;

    private void Awake() {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }
}
