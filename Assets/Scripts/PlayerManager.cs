using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public GameObject glowIndicator;
    bool isGlowIndicatorOn;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        SetGlowIndicator(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetGlowIndicator(bool value) {
        isGlowIndicatorOn = value;
        glowIndicator.SetActive(value);
    }
}
