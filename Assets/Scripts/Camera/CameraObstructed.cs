using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraObstructed : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Player; 

    public Obstruction[] Obstructions;

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
        RaycastHit[] hits;
        int obstructableLayerMask = 1 << 6; // Layer 6 is the Obstructable layer. 
        Ray ray = Camera.main.ScreenPointToRay(cam.WorldToScreenPoint(Player.position));
        
        hits = Physics.RaycastAll(ray.origin, ray.direction, Mathf.Infinity, obstructableLayerMask).OrderBy(h=>h.distance).ToArray();

        List<Obstruction> newObstructions = new List<Obstruction>();

        // Set alpha for obstructions
        foreach (RaycastHit hit in hits) {
            if(hit.collider.gameObject.tag == "Player"){
                // The ray reaches the player, the end of collider detection
                break;
            }

            Obstruction obstruction = hit.transform.gameObject.GetComponent<Obstruction>();
            if(obstruction.isObstructable()) {
                newObstructions.Add(obstruction);
                obstruction.setObstruct();
            }
        }

        // Reset alpha for previous obstructions
        IEnumerable<Obstruction> previousObstructions = Obstructions.Except(newObstructions.ToArray());
        foreach (Obstruction obstruction in previousObstructions) {
            obstruction.resetObstruct();
        }

        Obstructions = newObstructions.ToArray();
    }
}
