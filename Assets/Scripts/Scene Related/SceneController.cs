using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    public GameObject playerPrefab;
    List<int> scenesToLoad = new List<int>();
    List<AsyncOperation> sceneLoadingOperations = new List<AsyncOperation>();
    void Awake() {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else {
            DontDestroyOnLoad(gameObject);
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

    IEnumerator UnloadAllScenes() {
        SceneManager.LoadSceneAsync("Logic");
        yield return null;
        SceneManager.UnloadSceneAsync("Logic");
        yield return null;
    }
    IEnumerator LoadingScreen() {
        float progress = 0;
        for (int i = 0; i < sceneLoadingOperations.Count; i++)
        {
            while (!sceneLoadingOperations[i].isDone) {
                progress += sceneLoadingOperations[i].progress;
                float currentProgress = Mathf.Min(progress / sceneLoadingOperations.Count, 1f);
                Debug.Log(progress/sceneLoadingOperations.Count);
                yield return null;
            }
        }
        sceneLoadingOperations.Clear();
        scenesToLoad.Clear();
        yield return null;
    }
    public void UnloadScene(string sceneName) {
        SceneManager.UnloadSceneAsync(sceneName);
    }
    // Load at the corresponding scene during a reset process
    public IEnumerator ResetAtScene(string sceneName) {
        
        // To unload everything
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            var scene = SceneManager.GetSceneAt(i);
            // Do not reload logic
            if (scene.name.Equals("Logic")) continue;
            scenesToLoad.Add(scene.buildIndex);
            Debug.Log(scene.name);
        }
        yield return UnloadAllScenes();
        foreach (var sceneIndex in scenesToLoad) {
            sceneLoadingOperations.Add(SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive));
        }
        yield return LoadingScreen();
        Scene targetScene = SceneManager.GetSceneByName(sceneName);
        SceneManager.SetActiveScene(targetScene);
        SceneSettings settings = getSceneSettings(targetScene);
        if(settings == null) {
            Debug.LogWarning("Not Finding Scene Setting!");
            yield return null;
        }
        SetCameraPosition(settings.cameraPosition);
        SetWorldLightDirection(settings.worldLightDirection);
        DestroyPlayer();
        CreatePlayerOnPos(settings.playerSpawnPosition, settings.playerSpawnRotation);
        yield return null;
        
    }
    // Move to the scene. Load the scene if the scene is not loaded yet
    public IEnumerator SetActiveScene(string sceneName, bool setPlayerTransform) {
        Scene targetScene = SceneManager.GetSceneByName(sceneName);
        if (!targetScene.isLoaded) {
            sceneLoadingOperations.Add(SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive));
            StartCoroutine(LoadingScreen());
        }
        targetScene = SceneManager.GetSceneByName(sceneName);
        SceneManager.SetActiveScene(targetScene);
        SceneSettings settings = getSceneSettings(targetScene);
        if(settings == null) {
            Debug.LogWarning("Not Finding Scene Setting!");
            yield return null;
        }
        // Change camera position and directional light direction in "Basic Code" scene
        if (setPlayerTransform) {
            SetPlayerTransform(settings.playerSpawnPosition, settings.playerSpawnRotation);
        }
        SetCameraPosition(settings.cameraPosition);
        SetWorldLightDirection(settings.worldLightDirection);
        yield return null;
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
