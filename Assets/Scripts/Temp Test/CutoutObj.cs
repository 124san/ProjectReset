using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutoutObj : MonoBehaviour
{
    [SerializeField] Transform targetObject;
    [SerializeField] LayerMask wallMask;
    [SerializeField] float cutoutSize = 0.05f;
    [SerializeField] float falloffSize = 0.05f;
    private Camera mainCamera;
    private RaycastHit[] currHit;
    private void Awake() {
        mainCamera = GetComponent<Camera>();
    }

    private void Start() {
        targetObject = PlayerManager.instance.transform;
    }

    private void Update() {
        Vector2 cutoutPos = mainCamera.WorldToViewportPoint(targetObject.position);
        cutoutPos.y /= (Screen.width / Screen.height);
        Vector3 offset = targetObject.position - transform.position;
        RaycastHit[] hitObjects = Physics.RaycastAll(transform.position, offset, offset.magnitude, wallMask);
        if (hitObjects.Length == 0 && currHit != null) {
            // If raycast hits nothing, reset wall cutout
            for (int i=0; i < currHit.Length; i++) {
                Material[] materials = currHit[i].transform.GetComponent<Renderer>().materials;

                for (int j = 0; j < materials.Length; j++) {
                    materials[j].SetFloat("_CutoutSize", 0f);
                }
            }
            currHit = null;
        }
        else {
            currHit = hitObjects;
        }
        for (int i=0; i < hitObjects.Length; i++) {
            Material[] materials = hitObjects[i].transform.GetComponent<Renderer>().materials;

            for (int j = 0; j < materials.Length; j++) {
                materials[j].SetVector("_CutoutPosition", cutoutPos);
                materials[j].SetFloat("_CutoutSize", cutoutSize);
                materials[j].SetFloat("_FalloffSize", falloffSize);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draws a 5 unit long red line in front of the object
        Vector2 cutoutPos = mainCamera.WorldToViewportPoint(targetObject.position);
        cutoutPos.y /= (Screen.width / Screen.height);
        Vector3 offset = targetObject.position - transform.position;
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, offset);
    }
}
