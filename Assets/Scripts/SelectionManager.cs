using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
        if (Input.GetKeyDown("e") && currentlySelectedItem) {
            InteractableObject interactable = currentlySelectedItem.GetComponent<InteractableObject>();
            if (interactable) {
                interactable.HandleInteraction();
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        InteractableObject interactable = other.GetComponent<InteractableObject>();
        if (interactable) {
            currentlySelectedItem = other.gameObject;
            var selectionRendererChildren = other.GetComponentInChildren<Renderer>();
            if (selectionRendererChildren) {
                defaultMaterial = selectionRendererChildren.material;
                selectionRendererChildren.material = highlightMaterial;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (currentlySelectedItem == other.gameObject) {
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
