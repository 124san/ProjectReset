using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlowController : MonoBehaviour
{
    public static FlowController instance;
    public bool[] flags;
    public int[] variables;
    public static FlowController Instance {get {return instance;}}
    // Start is called before the first frame update
    
    private void Awake() {
        flags = new bool[200];
        variables = new int[200];
        if (instance != null && instance != this)
            Destroy(gameObject);
        else {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("r")) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void SetFlag(int index, bool value) {
        if (index > 0 && index < flags.Length) {
            flags[index] = value;
        }
    }

    public void SetVariable(int index, int value) {
        if (index > 0 && index < variables.Length) {
            variables[index] = value;
        }
    }
}
