using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDebugger : MonoBehaviour
{
    // Update is called once per frame
    [SerializeField]
    private GameObject playerPrefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1)) {
            destroyPlayer();
            setActiveScene("Room1");
            createPlayerOnPos(new Vector3(-0.5f, 2.02f, -13.5f), new Vector3(0.0f, 0.0f, 0.0f));
        } else if (Input.GetKeyDown(KeyCode.Keypad2)) {
            destroyPlayer();
            setActiveScene("Room2");
            createPlayerOnPos(new Vector3(0.5f, 2.02f, 44.5f), new Vector3(0.0f, 0.0f, 0.0f));
        } else if (Input.GetKeyDown(KeyCode.Keypad5)) {
            Application.LoadLevel(Application.loadedLevel);
        }
    }

    // This is just for redirect camera & change world light
    void setActiveScene(string sceneName) {
        // Scene previousScene = SceneManagement.GetActiveScene();
        Scene activating = SceneManager.GetSceneByName(sceneName);
        SceneManager.SetActiveScene(activating);

        SceneSettings settings = getSceneSettings(activating);
        if(settings == null) {
            Debug.LogWarning("Not Finding Scene Setting!");
            return;
        }

        Debug.Log("Changed to scene: " + activating.name);
        // Debug.Log(settings.cameraPosition);
        // Debug.Log(settings.worldLightDirection);

        // Change camera position
        changeCameraPosition(settings.cameraPosition);
        // Change directional light direction
        changeWorldLightDirection(settings.worldLightDirection);
    }

    void destroyPlayer() {
        // Scene currentScene = SceneManagement.GetActiveScene();
        PlayerManager instance = PlayerManager.instance;
        if(instance != null) {
            Destroy(instance.gameObject);
            PlayerManager.instance = null;
        }
    }

    void createPlayerOnPos(Vector3 playerPos, Vector3 playerDir) {
        Quaternion rotation = Quaternion.Euler(playerDir);
        Instantiate(playerPrefab, playerPos, rotation);
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
        Transform cameraPivotTransform = CameraPivot.instance.transform;
        cameraPivotTransform.position = pos;
    }

    // Change the directional light direction under "Basic Code"
    private void changeWorldLightDirection(Vector3 dir) {
        Transform worldLightTransform = WorldLight.instance.transform;
        worldLightTransform.rotation = Quaternion.Euler(dir);
    }
}
