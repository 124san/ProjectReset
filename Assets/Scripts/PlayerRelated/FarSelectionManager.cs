using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FarSelectionManager : MonoBehaviour
{
    [SerializeField] GameObject currentlySelectedItem;

    private void OnTriggerEnter(Collider other) {
        // Check if the colliding object is either an obstacle, box or slab.
        if (other.tag == "Obstacle" || other.tag == "Box" || other.tag == "Slab") {
            currentlySelectedItem = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (currentlySelectedItem == other.gameObject) {
            currentlySelectedItem = null;
        }
    }

    public void ResetSelection() {
        if (currentlySelectedItem != null)
            OnTriggerExit(currentlySelectedItem.GetComponent<Collider>());
    }

    public GameObject getCurrentlySelectedItem() {
        return currentlySelectedItem;
    }
}
