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
    bool canMove;
    public bool isSitting;

    public Vector3 nextPos, destination, direction;
    void Start()
    {
        currentDirection = up;
        nextPos = Vector3.forward;
        destination = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogueUI.instance.isOpen) {
            canMove = false;
            return;
        }
        if (isSitting) {
            // Placeholder for sit
            canMove = false;
            if(Input.GetKey(KeyCode.F)) {
                isSitting = false;
                destination = transform.position+new Vector3(0, 0, 1f);
                SelectionManager selectionManager = GetComponentInChildren<SelectionManager>();
                selectionManager.ResetSelection();
            }
        }
        else Move();
    }

    void Move() {
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        if (Vector3.Distance(destination, transform.position) > 0) return;
        if (Input.GetButton("Up")) {
            nextPos = Vector3.forward;
            currentDirection = up;
            canMove = true;
        }
        else if (Input.GetButton("Down")) {
            nextPos = Vector3.back;
            currentDirection = down;
            canMove = true;
        }
        else if (Input.GetButton("Right")) {
            nextPos = Vector3.right;
            currentDirection = right;
            canMove = true;
        }
        else if (Input.GetButton("Left")) {
            nextPos = Vector3.left;
            currentDirection = left;
            canMove = true;
        }

        transform.eulerAngles = currentDirection;
        if (canMove && ValidMovement()) {
            destination = transform.position + nextPos;
            direction = nextPos;
            canMove = false;
        }
    }

    bool ValidMovement() {
        Ray ray = new Ray(transform.position + new Vector3(0, -0.5f, 0), transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(transform.position+ new Vector3(0, -0.5f, 0), transform.forward, out hit, rayLength)) {
            if (hit.collider.CompareTag("Obstacle") || hit.collider.CompareTag("Door")) {
                return false;
            }
        }
        return true;
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
