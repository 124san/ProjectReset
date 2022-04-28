using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    void Awake() {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        DebugLoadAll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Temp method
    void DebugLoadAll() {
        // Load scene additively to have multiple scenes together
        SceneManager.LoadScene("Basic Code", LoadSceneMode.Additive);
        SceneManager.LoadScene("Room1", LoadSceneMode.Additive);
        SceneManager.LoadScene("Room2", LoadSceneMode.Additive);
    }

    // Load Scene additively given scene name
    void LoadSceneAdditive(string sceneName) {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }
}
