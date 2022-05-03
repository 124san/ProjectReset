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
        // DebugLoadAll();
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
    public void UnloadScene(string sceneName) {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    // Move to the scene. Load the scene if the scene is not loaded yet
    public void SetActiveScene(string sceneName) {
        Scene targetScene = SceneManager.GetSceneByName(sceneName);
        if (!targetScene.isLoaded) {
            LoadSceneAdditive(sceneName);
        }
        SceneManager.SetActiveScene(targetScene);
        SceneSettings settings = getSceneSettings(targetScene);
        if(settings == null) {
            Debug.LogWarning("Not Finding Scene Setting!");
            return;
        }
        // Change camera position and directional light direction in "Basic Code" scene
        changeCameraPosition(settings.cameraPosition);
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
        Transform cameraPivotTransform = CameraPivot.instance.transform;
        cameraPivotTransform.position = pos;
    }

    // Change the directional light direction under "Basic Code"
    private void changeWorldLightDirection(Vector3 dir) {
        Transform worldLightTransform = WorldLight.instance.transform;
        worldLightTransform.rotation = Quaternion.Euler(dir);
    }
}
