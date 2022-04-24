using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstruction : MonoBehaviour
{

    [SerializeField, Range(0.0f, 1.0f)] float obstructionAlpha;
    [SerializeField] bool obstructable;

    private Material wallMaterial;
    // Start is called before the first frame update
    void Awake()
    {
        wallMaterial = gameObject.GetComponent<MeshRenderer>().material;
    }

    public void setObstruct() {
        setAlpha(obstructionAlpha);
    }

    public void resetObstruct() {
        setAlpha(1.0f);
    }

    public bool isObstructable() {
        return obstructable;
    }

    private void setColor(Color newColor){
        wallMaterial.color = newColor;
    }

    private void setAlpha(float alpha){
        Color newColor = wallMaterial.color;
        newColor.a = alpha;
        wallMaterial.color = newColor;
    }
}
