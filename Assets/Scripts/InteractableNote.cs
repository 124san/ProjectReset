using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableNote : InteractableObject
{
    public List<string> dialogue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void HandleInteraction()
    {
        //TODO make this in UI later
        throw new System.NotImplementedException();
    }
}
