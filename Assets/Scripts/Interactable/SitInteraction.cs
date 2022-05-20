using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitInteraction : InteractableObject
{
    [SerializeField] float sitTriggerTime = 2f;
    [SerializeField] Transform sitPivot;
    private float sitCountDown;
    GridMovement player;
    // Start is called before the first frame update
    void Start()
    {
        sitCountDown = sitTriggerTime;
    }

    // Update is called once per frame
    void Update()
    {
        player = PlayerManager.instance.GetComponent<GridMovement>();
        if (!player.isSitting) {
            this.GetComponent<Collider>().enabled = true;
        }
    }

    public override void HandleInteraction() {
        player = PlayerManager.instance.GetComponent<GridMovement>();
        if(player.isSitting) {
            Debug.Log("Standing up");
            // TODO: Set player move condition to True
            // player.SetCanMove(true);
            player.isSitting = false;
            player.destination = player.transform.position+new Vector3(0, 0, 1f);
            SelectionManager selectionManager = player.GetComponentInChildren<SelectionManager>();
            selectionManager.ResetSelection();
        }
        else {
            Debug.Log("Sitting down");
            // TODO: Set player move condition to False
            // TODO: Change player direction when sitting down (and standing up if needed)
            // player.SetCanMove(false);
            player.isSitting = true;
            player.direction = new Vector3(0, 0, 1);
            this.GetComponent<Collider>().enabled = false;
            player.transform.eulerAngles = Vector3.zero;
            player.transform.position = new Vector3(sitPivot.position.x, player.transform.position.y, sitPivot.position.z);
        }
    }
    
}
