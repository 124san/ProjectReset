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
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }

    private void Update() {
        if(Input.GetKeyDown("r")) {
            StartCoroutine(Reset("Room1"));
        }
    }
    // This method handles every reset related operations, including
    /*
    * Sending player to the reset point's scene and position
    * Reset turn to 1
    * Reset flags. NoResetFlags will not be reset.
    * Delete inventory items that are resetting
    */
    public IEnumerator Reset(string sceneName) {
        TransitionAnimation.instance.FadeOut();
        yield return new WaitForSeconds(1f);
        TurnManager.instance.ResetTurn();
        FlowController.instance.ResetFlags();
        InventorySystem.instance.OnReset();
        yield return SceneController.instance.ResetAtScene(sceneName);
        TransitionAnimation.instance.FadeIn();
        yield return new WaitForSeconds(1f);
    }
}
