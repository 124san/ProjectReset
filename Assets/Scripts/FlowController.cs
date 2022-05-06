using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlowController : MonoBehaviour
{
    public static FlowController instance;
    [SerializeField] bool[] flags = new bool[200];
    [SerializeField] bool[] noResetFlags = new bool [200];
    [SerializeField] int[] variables = new int [200];
    public static FlowController Instance {get {return instance;}}
    
    private void Awake() {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }

    // Update is called once per frame

    public void SetFlag(int index, bool value) {
        if (index > 0 && index < flags.Length) {
            flags[index] = value;
        }
    }

    public void SetNoResetFlag(int index, bool value) {
        if (index > 0 && index < noResetFlags.Length) {
            noResetFlags[index] = value;
        }
    }

    public void SetVariable(int index, int value) {
        if (index > 0 && index < variables.Length) {
            variables[index] = value;
        }
    }

    public bool GetFlag(int index) {
        if (index > 0 && index < flags.Length) {
            return flags[index];
        }
        return false;
    }
    public bool GetNoResetFlag(int index) {
        if (index > 0 && index < flags.Length) {
            return flags[index];
        }
        return false;
    }
    public int GetVariable(int index) {
        if (index > 0 && index < variables.Length) {
            return variables[index];
        }
        return -1;
    }

    public void ResetFlags() {
        for (int i = 0; i < flags.Length; i++)
        {
            flags[i] = false;
        }
    }
}
