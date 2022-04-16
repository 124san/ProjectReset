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
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<GridMovement>();
        sitCountDown = sitTriggerTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.isSitting) {
            this.GetComponent<Collider>().enabled = true;
        }
    }

    public override void HandleInteraction() {
        player.isSitting = true;
        this.GetComponent<Collider>().enabled = false;
        player.transform.eulerAngles = Vector3.zero;
        player.transform.position = new Vector3(sitPivot.position.x, player.transform.position.y, sitPivot.position.z);
    }
    
}
