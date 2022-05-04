using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetSystem : MonoBehaviour
{
    public static ResetSystem instance;

    // Start is called before the first frame update
    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this;
        }
    }

    private void Update() {
        if(Input.GetKeyDown("r")) {
            StartCoroutine(Reset("Room1"));
        }
    }

    public IEnumerator Reset(string sceneName) {
        TransitionAnimation.instance.TriggerAnimation();
        yield return new WaitForSeconds(1f);
        FlowController.instance.ResetFlags();
        InventorySystem.instance.OnReset();
        SceneController.instance.ResetAtScene(sceneName);
        yield return new WaitForSeconds(1f);
    }
}
