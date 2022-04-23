using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraObstructed : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Player; 

    public Transform Obstruction;

    public Camera cam;

    [SerializeField, Range(0.0f, 1.0f)] float ObstructredAlpha;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        ViewObstructed();
    }

    void ViewObstructed() {
        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(cam.WorldToScreenPoint(Player.position));

        if(Physics.Raycast(ray.origin, ray.direction * 10, out hit)){
            Debug.Log(string.Format("Hit object: {0}, Hit location: {1}", hit.transform.name, hit.transform.position));
            Debug.Log(string.Format("Player position: {0}", Player.position));
            // Debug.DrawRay(ray.origin, ray.direction * 10, Color.green);
            if(hit.collider.gameObject.tag == "Obstacle") {
                Obstruction = hit.transform;
                // Debug.Log()
                Obstruction.gameObject.GetComponent<WallTextureManager>().setAlpha(ObstructredAlpha);
            } else {
                if(Obstruction != null) {
                    Obstruction.gameObject.GetComponent<WallTextureManager>().setAlpha(1.0f);
                }
            }
        }

    }
}
