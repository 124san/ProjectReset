using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSettings : MonoBehaviour
{
    [SerializeField] 
    public Vector3 cameraPosition;

    [SerializeField]
    public Vector3 worldLightDirection;
    [SerializeField]
    public Vector3 playerSpawnPosition = new Vector3(0.5f, 2.02f, 0.5f);
    [SerializeField]
    public Vector3 playerSpawnRotation;
}
