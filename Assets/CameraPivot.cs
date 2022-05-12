using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivot : MonoBehaviour
{
    public static CameraPivot instance;
    private Camera cam;
    public Camera currentCamera => cam;

    private void Awake() {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Start() {
        cam = GetComponentInChildren<Camera>();
    }

    // Similar to Camera.WorldToScreenPoint, but use center as pivot
    public Vector2 WorldToScreenPointCenterPivot(Vector3 position) {
        var screenPoint = cam.WorldToScreenPoint(position);
        var width = Screen.currentResolution.width;
        var height = Screen.currentResolution.height;
        return new Vector2(screenPoint.x - width/2.0f, screenPoint.y - height/2.0f);
    }
}
