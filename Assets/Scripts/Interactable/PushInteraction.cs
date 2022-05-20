using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushInteraction : InteractableObject
{
    private bool isPushing; // pushing animation/transforamti
    private Vector3 destination;

    [SerializeField] float speed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        isPushing = false;
    }

    // Update is called once per frame
    void FixedUpdate() 
    {
        if(isPushing) 
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);

            // Check if the position of the cube and sphere are approximately equal.
            if (Vector3.Distance(transform.position, destination) < 0.001f) {
                transform.position = destination;
                isPushing = false;
                PlayerManager.instance.GetComponent<GridMovement>().takeMovementInput = true;
            }
        }
    }

    public override void HandleInteraction() 
    {
        GridMovement player = PlayerManager.instance.GetComponent<GridMovement>();
        FarSelectionManager targetSelector = player.GetComponentInChildren<FarSelectionManager>();

        // Debug.Log(player.transform.position - gameObject.transform.position);

        if(targetSelector.getCurrentlySelectedItem() == null && !isPushing) {
            Vector3 dir = gameObject.transform.position - player.transform.position;
            dir.y = 0;
            dir = dir.normalized;
            // Debug.Log(dir);
            Push(dir);
        }
    }

    // push the object in the dirction
    private void Push(Vector3 dir) {
        // Start to move the box's transform
        
        // NOTE: the following one is a dummy testing simple way
        // TODO: complete it with animation similar as player
        PlayerManager.instance.GetComponent<GridMovement>().takeMovementInput = false;
        isPushing = true;
        destination = transform.position + dir;
    }
}
