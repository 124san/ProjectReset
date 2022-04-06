using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoResetObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (NoResetManager.instance) {
            gameObject.transform.SetParent(NoResetManager.instance.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
