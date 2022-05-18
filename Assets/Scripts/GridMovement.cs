using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 up = Vector3.zero;
    Vector3 right = new Vector3(0, 90, 0), down = new Vector3(0, 180, 0), left = new Vector3(0, 270, 0);
    Vector3 currentDirection = Vector3.zero;

    [SerializeField] float speed = 5f;
    float rayLength = 1f;
    // Check if the player is going to move after taking inputs
    bool goingToMove;
    // Only take movement input if this bool is set to true
    public bool takeMovementInput;
    public bool isSitting;

    public Vector3 nextPos, destination, direction;
    void Start()
    {
        takeMovementInput = true;
        currentDirection = up;
        nextPos = Vector3.forward;
        destination = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Controlable()) {
            MoveInputs();
            PerformMove(); // For debugging purpose. Should be in FixedUpdate()
        }
    }

    // void FixedUpdate() 
    // {
    //     PerformMove();
    // }

    // Function for checking the movement input
    // Should be called in Update()
    void MoveInputs() {
        if (!takeMovementInput || Vector3.Distance(destination, transform.position) > 0) return;
        
        if (Input.GetButton("Up")) {
            nextPos = Vector3.forward;
            currentDirection = up;
            goingToMove = true;
        }
        else if (Input.GetButton("Down")) {
            nextPos = Vector3.back;
            currentDirection = down;
            goingToMove = true;
        }
        else if (Input.GetButton("Right")) {
            nextPos = Vector3.right;
            currentDirection = right;
            goingToMove = true;
        }
        else if (Input.GetButton("Left")) {
            nextPos = Vector3.left;
            currentDirection = left;
            goingToMove = true;
        }

        
    }

    // Transform/Move the character on the grid
    // Should be called in FixedUpdate() for performance
    void PerformMove()
    {
        // Turn character rotation
        transform.eulerAngles = currentDirection;

        if (goingToMove && ValidMovement()) {
            destination = transform.position + nextPos;
            direction = nextPos;
            goingToMove = false;
        } else {
            nextPos = Vector3.zero;
        }

        // Transform character position
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
    }

    bool ValidMovement() {
        Ray ray = new Ray(transform.position + new Vector3(0, -0.5f, 0), transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(transform.position+ new Vector3(0, -0.5f, 0), transform.forward, out hit, rayLength)) {
            if (hit.collider.CompareTag("Obstacle") || hit.collider.CompareTag("Door") || hit.collider.CompareTag("Box")) {
                return false;
            }
        }
        return true;
    }

    bool Controlable() {
        return !(isSitting || DialogueUI.instance.isOpen);
    }

    void OnDrawGizmosSelected()
    {
        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.red;
        Vector3 direction = transform.forward * rayLength;
        Gizmos.DrawRay(transform.position + new Vector3(0, -0.5f, 0), direction);
    }
    
    public void SetPosition(Vector3 pos) {
        this.transform.position = pos;
        destination = pos;
    }
}
