using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutoutObject : MonoBehaviour
{
    [SerializeField]
    private Transform targetObject;

    [SerializeField]
    private LayerMask wallMask;

    [SerializeField]
    private float cutoutSize = 0.1f;
    [SerializeField, Range(0,1)]
    private float falloffSizeRatio = 0.5f;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        Vector2 cutoutPos = mainCamera.WorldToViewportPoint(targetObject.position);
        cutoutPos.y /= (Screen.width / Screen.height);

        Vector3 offset = targetObject.position - transform.position;
        RaycastHit[] hitObjects = Physics.RaycastAll(transform.position, offset, offset.magnitude, wallMask);

        for (int i = 0; i < hitObjects.Length; ++i)
        {
            Material[] materials = hitObjects[i].transform.GetComponent<Renderer>().materials;

            for(int m = 0; m < materials.Length; ++m)
            {
                Debug.Log(materials[m]);
                materials[m].SetVector("_CutoutPos", cutoutPos);
                materials[m].SetFloat("_CutoutSize", cutoutSize);
                materials[m].SetFloat("_FalloffSize", cutoutSize * falloffSizeRatio);
            }
        }
    }
}
