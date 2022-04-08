using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] GameObject currentlySelectedItem;
    [SerializeField] Material highlightMaterial;
    [SerializeField] Material defaultMaterial;
    string[] interactableTags = {"InventoryItem", "Door"};
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e") && currentlySelectedItem) {
            switch (currentlySelectedItem.tag) {
                case "InventoryItem":
                    currentlySelectedItem.GetComponent<ItemObject>().OnHandlePickupItem();
                    break;
                case "Door":
                    Door door = currentlySelectedItem.GetComponent<Door>();
                    if (door) {
                        door.OpenDoorOneTimeUse();
                    }
                        
                    break;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        
        if (Array.Exists(interactableTags, element => element == other.tag)) {
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
