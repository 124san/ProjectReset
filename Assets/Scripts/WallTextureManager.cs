using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTextureManager : MonoBehaviour
{

    [SerializeField] Color color;
    [SerializeField, Range(0.0f, 1.0f)] float alpha;

    private Material wallMaterial;
    // Start is called before the first frame update
    void Awake()
    {
        wallMaterial = gameObject.GetComponent<MeshRenderer>().material;

        Color wallColor = color;
        wallColor.a = alpha;
        wallMaterial.color = wallColor;
    }

    public void setColor(Color newColor){
        wallMaterial.color = newColor;
    }

    public void setAlpha(float alpha){
        Color newColor = wallMaterial.color;
        newColor.a = alpha;
        wallMaterial.color = newColor;
    }
}
