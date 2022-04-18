using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] string sceneName = "";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        
        if (other.CompareTag("Player")) {
            Debug.Log(other);
            SceneManager.LoadScene(sceneName);
        }
    }
}
