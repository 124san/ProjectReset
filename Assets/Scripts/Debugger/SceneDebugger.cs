using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDebugger : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1)) {
            setActiveScene("Room1");
        } else if (Input.GetKeyDown(KeyCode.Keypad2)) {
            setActiveScene("Room2");
        }
    }

    void setActiveScene(string sceneName) {
        Scene activating = SceneManager.GetSceneByName(sceneName);
        SceneManager.SetActiveScene(activating);

        SceneSettings settings = getSceneSettings(activating);
        if(settings == null) {
            Debug.LogWarning("Not Finding Scene Setting!");
            return;
        }

        Debug.Log(settings.cameraPosition);
        Debug.Log(settings.worldLightDirection);

        // Change camera position
        changeCameraPosition(settings.cameraPosition);
        // Change directional light direction
        changeWorldLightDirection(settings.worldLightDirection);
    }

    // Return Scene Settings given a Scene object
    private SceneSettings getSceneSettings(Scene scene) {
        GameObject[] gameObjects = scene.GetRootGameObjects();
        foreach (GameObject gameObject in gameObjects) {
            if(gameObject.name == "Scene Settings") {
                return gameObject.GetComponent<SceneSettings>();
            }
        }
        return null;
    }

    // Change the camera pivot position under "Basic Code"
    private void changeCameraPosition(Vector3 pos) {
        GameObject cameraPivot = GameObject.Find("CameraPivot");
        cameraPivot.transform.position = pos;
    }

    // Change the directional light direction under "Basic Code"
    private void changeWorldLightDirection(Vector3 dir) {
        GameObject worldLight = GameObject.Find("Directional Light");
        worldLight.transform.rotation = Quaternion.identity;
        worldLight.transform.Rotate(dir);
    }
}
