  using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class IconGenerator : MonoBehaviour
{
    public List<GameObject> sceneObjects;
    public List<InventoryItemData> dataObjects;
    [SerializeField] string prefix = "test";
    [SerializeField] string pathFolder = "Icons";
    Camera thisCamera;

    private void Awake() {
        thisCamera = GetComponent<Camera>();
    }

    [ContextMenu("Screenshot")]
    private void ProcessScreenshots() {
        StartCoroutine(Screenshot());
    }


    private IEnumerator Screenshot() {
        for (int i=0; i < sceneObjects.Count; i++) {
            GameObject obj = sceneObjects[i];
            InventoryItemData data = dataObjects[i];
            obj.gameObject.SetActive(true);
            yield return null;
            TakeScreenshot($"{Application.dataPath}/{pathFolder}/{data.id}_Icon.png");
            yield return null;
            obj.gameObject.SetActive(false);
            Sprite s = AssetDatabase.LoadAssetAtPath<Sprite>($"Assets/{pathFolder}/{data.id}_Icon.png");
            if (s != null) {
                data.icon = s;
                EditorUtility.SetDirty(data);
            }
            yield return null;
        }
    }

    // Start is called before the first frame update
    void TakeScreenshot(string fullPath) {
        thisCamera = GetComponent<Camera>();
        RenderTexture rt = new RenderTexture(256, 256, 24);
        thisCamera.targetTexture = rt;
        Texture2D screenShot = new Texture2D(256, 256, TextureFormat.RGBA32, false);
        thisCamera.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, 256, 256), 0, 0);
        thisCamera.targetTexture = null;
        RenderTexture.active = null;

        if (Application.isEditor) {
            DestroyImmediate(rt);
        }
        else {
            Destroy(rt);
        }

        byte[] bytes = screenShot.EncodeToPNG();
        System.IO.File.WriteAllBytes(fullPath, bytes);
#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }

    
}
