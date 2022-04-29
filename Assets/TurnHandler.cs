using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurnHandler : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    public TurnEvent[] events;

    private UnityEvent[] eventList;

    void Start()
    {
        eventList = new UnityEvent[TurnManager.instance.getMaxTurn()];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
