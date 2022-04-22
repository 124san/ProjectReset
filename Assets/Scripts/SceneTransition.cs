using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] string sceneName = "";
    [SerializeField] bool moveY = false;
    [SerializeField] Vector3 destination = new Vector3(0, 2.02f, 0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        
        if (other.CompareTag("Player")) {
            Debug.Log(other);
            GridMovement player = PlayerManager.instance.GetComponent<GridMovement>();
            SceneManager.LoadScene(sceneName);
            Vector3 targetPosition = new Vector3(destination.x, moveY ? destination.y : player.transform.position.y, destination.z);
            player.SetPosition(targetPosition);
        }
    }
}
