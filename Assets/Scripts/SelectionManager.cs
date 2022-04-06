using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] GameObject currentlySelectedItem;
    [SerializeField] Material highlightMaterial;
    [SerializeField] Material defaultMaterial;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e")) {
            if (currentlySelectedItem) {
                currentlySelectedItem.GetComponent<ItemObject>().OnHandlePickupItem();
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("InventoryItem")) {
            currentlySelectedItem = other.gameObject;
            var selectionRendererChildren = other.GetComponentInChildren<Renderer>();
            if (selectionRendererChildren) {
                defaultMaterial = selectionRendererChildren.material;
                selectionRendererChildren.material = highlightMaterial;
            }
            
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("InventoryItem")) {
            var selectionRenderer = other.GetComponent<Renderer>();
            if (selectionRenderer) {
                selectionRenderer.material = defaultMaterial;}
            var selectionRendererChildren = other.GetComponentInChildren<Renderer>();
            if (selectionRendererChildren) {
                selectionRendererChildren.material = defaultMaterial;
            }
            currentlySelectedItem = null;
            defaultMaterial = null;
        }
    }
}
