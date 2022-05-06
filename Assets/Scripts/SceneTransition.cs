using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] string sceneName = "";
    [SerializeField] bool moveY = false;
    [SerializeField] Vector3 destination = new Vector3(0, 2.02f, 0);
    float transitionDelay = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        
        if (other.CompareTag("Player")) {
            TransitionAnimation.instance.TriggerAnimation();
            GridMovement player = PlayerManager.instance.GetComponent<GridMovement>();
            StartCoroutine(MovePlayerWithDelay(transitionDelay, player));
        }
    }

    IEnumerator MovePlayerWithDelay(float delay, GridMovement player) {
        yield return new WaitForSeconds(delay);
        
        Vector3 targetPosition = new Vector3(destination.x, moveY ? destination.y : player.transform.position.y, destination.z);
        yield return SceneController.instance.SetActiveScene(sceneName, false);
        SceneController.instance.DestroyPlayer();
        SceneController.instance.CreatePlayerOnPos(targetPosition, Vector3.zero);
        yield return new WaitForSeconds(delay);
    }
}
