using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldLight : MonoBehaviour
{
    public static WorldLight instance;
    private Light light;

    private void Awake() {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else {
            instance = this;
            light = this.GetComponent<Light>();
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void changeLightDir(Vector3 dir) {
        this.transform.rotation = Quaternion.Euler(dir);
    }

    public void changeLightIntensity(float intensity) {
        light.intensity = intensity;
    }

    public void changeLightcolour(Color color) {
        light.color = color;
    }
}
