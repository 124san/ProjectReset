using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    public GameObject playerPrefab;
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
    // Load at the corresponding scene during a reset process
    public void ResetAtScene(string sceneName) {
        Scene targetScene = SceneManager.GetSceneByName(sceneName);
        LoadSceneAdditive(sceneName);
        SceneManager.SetActiveScene(targetScene);
        SceneSettings settings = getSceneSettings(targetScene);
        if(settings == null) {
            Debug.LogWarning("Not Finding Scene Setting!");
            return;
        }
        SetCameraPosition(settings.cameraPosition);
        SetWorldLightDirection(settings.worldLightDirection);
        DestroyPlayer();
        CreatePlayerOnPos(settings.playerSpawnPosition, settings.playerSpawnRotation);
        
    }
    // Move to the scene. Load the scene if the scene is not loaded yet
    public void SetActiveScene(string sceneName, bool setPlayerTransform) {
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
        if (setPlayerTransform) {
            SetPlayerTransform(settings.playerSpawnPosition, settings.playerSpawnRotation);
        }
        SetCameraPosition(settings.cameraPosition);
        SetWorldLightDirection(settings.worldLightDirection);

    }

    public void DestroyPlayer() {
        // Scene currentScene = SceneManagement.GetActiveScene();
        PlayerManager instance = PlayerManager.instance;
        if(instance != null) {
            Destroy(instance.gameObject);
            PlayerManager.instance = null;
        }
    }

    public void CreatePlayerOnPos(Vector3 playerPos, Vector3 playerDir) {
        Quaternion rotation = Quaternion.Euler(playerDir);
        GameObject player = Instantiate(playerPrefab, playerPos, rotation);
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
    private void SetCameraPosition(Vector3 pos) {
        Transform cameraPivotTransform = CameraPivot.instance.transform;
        cameraPivotTransform.position = pos;
    }

    // Change the directional light direction under "Basic Code"
    private void SetWorldLightDirection(Vector3 dir) {
        Transform worldLightTransform = WorldLight.instance.transform;
        worldLightTransform.rotation = Quaternion.Euler(dir);
    }
    private void SetPlayerTransform(Vector3 pos, Vector3 angle) {
        GridMovement player = PlayerManager.instance.GetComponent<GridMovement>();
        player.SetPosition(pos);
        player.transform.eulerAngles = angle;
    }
}
